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
    /// State verileri için servis sınıfı
    /// </summary>
    public class StateService : IStateService
    {
        private readonly ILogger<StateService> _logger;
        private readonly IConfiguration _configuration;

        public StateService(ILogger<StateService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<StateResponse>> GetStatesAsync(string langCode)
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection"));
                await connection.OpenAsync();

                var sql = @"
                    SELECT
                        StateCode = RTRIM(LTRIM(cdState.StateCode)),
                        CountryCode = RTRIM(LTRIM(cdState.CountryCode)),
                        cdState.IsBlocked,
                        @LangCode AS LangCode,
                        StateDescription = RTRIM(LTRIM(ISNULL(sd.StateDescription, '')))
                    FROM cdState cdState WITH (NOLOCK)
                        LEFT JOIN cdStateDesc sd WITH (NOLOCK)
                            ON sd.StateCode = cdState.StateCode
                            AND sd.LangCode = @LangCode";

                var result = await connection.QueryAsync<StateResponse>(sql, new { LangCode = langCode });
                return result.ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogWarning(ex, "Database error occurred while getting states. Will return empty list.");
                return new List<StateResponse>();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting states. Will return empty list.");
                return new List<StateResponse>();
            }
        }
    }
}
