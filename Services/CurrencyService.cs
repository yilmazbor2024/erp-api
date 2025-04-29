using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Linq;
using ErpMobile.Api.Interfaces;
using erp_api.Models.Responses;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Para birimi servisi
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly ILogger<CurrencyService> _logger;
        private readonly IConfiguration _configuration;

        public CurrencyService(ILogger<CurrencyService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Para birimi listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Para birimi listesi</returns>
        public async Task<List<CurrencyResponse>> GetCurrenciesAsync(string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT
                            CurrencyCode,
                            CurrencyDescription,
                            LangCode,
                            IsBlocked
                        FROM Currency(@LangCode)
                        WHERE IsBlocked = 0
                        ORDER BY CurrencyCode";

                    var currencies = await connection.QueryAsync<CurrencyResponse>(sql, new { LangCode = langCode });
                    return currencies.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Para birimleri alınırken hata oluştu. Dil Kodu: {LangCode}", langCode);
                throw;
            }
        }

        /// <summary>
        /// Para birimi detayını getirir
        /// </summary>
        /// <param name="currencyCode">Para birimi kodu</param>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Para birimi detayı</returns>
        public async Task<CurrencyResponse> GetCurrencyByCodeAsync(string currencyCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT
                            CurrencyCode,
                            CurrencyDescription,
                            LangCode,
                            IsBlocked
                        FROM Currency(@LangCode)
                        WHERE CurrencyCode = @CurrencyCode";

                    var currency = await connection.QueryFirstOrDefaultAsync<CurrencyResponse>(sql, 
                        new { LangCode = langCode, CurrencyCode = currencyCode });
                    
                    return currency;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Para birimi detayı alınırken hata oluştu. Para Birimi Kodu: {CurrencyCode}, Dil Kodu: {LangCode}", 
                    currencyCode, langCode);
                throw;
            }
        }
    }
} 