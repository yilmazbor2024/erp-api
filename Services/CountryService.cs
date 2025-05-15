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
    /// Ülke verileri için servis sınıfı
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly ILogger<CountryService> _logger;
        private readonly IConfiguration _configuration;

        public CountryService(ILogger<CountryService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<CountryResponse>> GetCountriesAsync(string langCode)
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();

                var sql = @"
                    SELECT
                        CountryCode = RTRIM(LTRIM(cdCountry.CountryCode)),
                        CurrencyCode = RTRIM(LTRIM(cdCountry.CurrencyCode)),
                        cdCountry.UseVat,
                        cdCountry.IsVatRequired,
                        TaxDecCode = RTRIM(LTRIM(cdCountry.TaxDecCode)),
                        CountryISOCode = RTRIM(LTRIM(cdCountry.CountryISOCode)),
                        cdCountry.UseItemDim1Equ,
                        cdCountry.IsItemDim1Required,
                        cdCountry.UseItemDim2Equ,
                        cdCountry.IsItemDim2Required,
                        cdCountry.UseItemDim3Equ,
                        cdCountry.IsItemDim3Required,
                        cdCountry.ApplyPCTOnSelectedPaymentTypes,
                        cdCountry.IsBlocked,
                        @LangCode AS LangCode,
                        CountryDescription = RTRIM(LTRIM(ISNULL(cdCountryDesc.CountryDescription, '')))
                    FROM cdCountry cdCountry WITH (NOLOCK)
                        LEFT JOIN cdCountryDesc cdCountryDesc WITH (NOLOCK)
                            ON cdCountryDesc.CountryCode = cdCountry.CountryCode
                            AND cdCountryDesc.LangCode = @LangCode";

                var result = await connection.QueryAsync<CountryResponse>(sql, new { LangCode = langCode });
                return result.ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogWarning(ex, "Database error occurred while getting countries. Will return empty list.");
                return new List<CountryResponse>();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting countries. Will return empty list.");
                return new List<CountryResponse>();
            }
        }
    }
}
