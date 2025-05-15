using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Provides hierarchical location data: Country -> State -> City -> District
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly ILogger<LocationService> _logger;
        private readonly IConfiguration _configuration;

        public LocationService(ILogger<LocationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<LocationHierarchyResponse> GetLocationHierarchyAsync(string langCode, string countryCode)
        {
            const string sql = @"
                SELECT
                    cdDistrict.DistrictCode AS DistrictCode,
                    ISNULL(dd.DistrictDescription, '') AS DistrictDescription,
                    cdCity.CityCode AS CityCode,
                    ISNULL(cdCityDesc.CityDescription, '') AS CityDescription,
                    cdState.StateCode AS StateCode,
                    ISNULL(sd.StateDescription, '') AS StateDescription,
                    cdState.CountryCode AS CountryCode,
                    ISNULL(cdCountryDesc.CountryDescription, '') AS CountryDescription,
                    cdDistrict.IsBlocked AS IsBlocked
                FROM cdDistrict cdDistrict WITH(NOLOCK)
                INNER JOIN cdCity cdCity WITH(NOLOCK) ON cdCity.CityCode = cdDistrict.CityCode
                INNER JOIN cdState cdState WITH(NOLOCK) ON cdState.StateCode = cdCity.StateCode
                LEFT JOIN cdDistrictDesc dd WITH(NOLOCK) ON dd.DistrictCode = cdDistrict.DistrictCode AND dd.LangCode = @LangCode
                LEFT JOIN cdCityDesc cdCityDesc WITH(NOLOCK) ON cdCityDesc.CityCode = cdCity.CityCode AND cdCityDesc.LangCode = @LangCode
                LEFT JOIN cdStateDesc sd WITH(NOLOCK) ON sd.StateCode = cdState.StateCode AND sd.LangCode = @LangCode
                LEFT JOIN cdCountryDesc cdCountryDesc WITH(NOLOCK) ON cdCountryDesc.CountryCode = cdState.CountryCode AND cdCountryDesc.LangCode = @LangCode
                WHERE cdDistrict.DistrictCode <> SPACE(0) AND cdState.CountryCode = @CountryCode";

            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();

                var items = await connection.QueryAsync<LocationFlatten>(sql, new { LangCode = langCode, CountryCode = countryCode });
                // Group into hierarchy
                var first = items.FirstOrDefault();
                if (first == null)
                {
                    return new LocationHierarchyResponse { CountryCode = countryCode, CountryDescription = string.Empty, States = new List<StateDto>() };
                }
                var states = items
                    .GroupBy(i => new { i.StateCode, i.StateDescription })
                    .Select(g => new StateDto
                    {
                        StateCode = g.Key.StateCode,
                        StateDescription = g.Key.StateDescription,
                        Cities = g.GroupBy(x => new { x.CityCode, x.CityDescription })
                                  .Select(gc => new CityDto
                                  {
                                      CityCode = gc.Key.CityCode,
                                      CityDescription = gc.Key.CityDescription,
                                      Districts = gc.Select(x => new DistrictDto
                                      {
                                          DistrictCode = x.DistrictCode,
                                          DistrictDescription = x.DistrictDescription,
                                          IsBlocked = x.IsBlocked
                                      }).ToList()
                                  }).ToList()
                    }).ToList();

                return new LocationHierarchyResponse
                {
                    CountryCode = first.CountryCode,
                    CountryDescription = first.CountryDescription,
                    States = states
                };
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting location hierarchy for {CountryCode}", countryCode);
                throw;
            }
        }

        private class LocationFlatten
        {
            public string DistrictCode { get; set; }
            public string DistrictDescription { get; set; }
            public string CityCode { get; set; }
            public string CityDescription { get; set; }
            public string StateCode { get; set; }
            public string StateDescription { get; set; }
            public string CountryCode { get; set; }
            public string CountryDescription { get; set; }
            public bool IsBlocked { get; set; }
        }
    }
}
