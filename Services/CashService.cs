using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public class CashService : ICashService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CashService> _logger;

        public CashService(IConfiguration configuration, ILogger<CashService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Tüm kasa hareketlerini getirir
        /// </summary>
        public async Task<List<CashTransactionResponse>> GetAllCashTransactionsAsync(string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"SELECT * FROM AllCashs WITH(NOLOCK)";

                    var cashTransactions = await connection.QueryAsync<CashTransactionResponse>(sql);
                    return cashTransactions.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving all cash transactions");
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all cash transactions");
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir para birimine ait kasa hareketlerini getirir
        /// </summary>
        public async Task<List<CashTransactionResponse>> GetCashTransactionsByCurrencyAsync(string currencyCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM AllCashs WITH(NOLOCK)
                        WHERE Doc_CurrencyCode = @CurrencyCode";

                    var cashTransactions = await connection.QueryAsync<CashTransactionResponse>(sql, new { CurrencyCode = currencyCode });
                    return cashTransactions.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving cash transactions by currency: {CurrencyCode}", currencyCode);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash transactions by currency: {CurrencyCode}", currencyCode);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir hareket tipine ait kasa hareketlerini getirir
        /// </summary>
        public async Task<List<CashTransactionResponse>> GetCashTransactionsByTypeAsync(int cashTransTypeCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM AllCashs WITH(NOLOCK)
                        WHERE CashTransTypeCode = @CashTransTypeCode";

                    var cashTransactions = await connection.QueryAsync<CashTransactionResponse>(sql, new { CashTransTypeCode = cashTransTypeCode });
                    return cashTransactions.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving cash transactions by type: {CashTransTypeCode}", cashTransTypeCode);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash transactions by type: {CashTransTypeCode}", cashTransTypeCode);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir para birimi ve hareket tipine ait kasa hareketlerini getirir
        /// </summary>
        public async Task<List<CashTransactionResponse>> GetCashTransactionsByCurrencyAndTypeAsync(string currencyCode, int cashTransTypeCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM AllCashs WITH(NOLOCK)
                        WHERE Doc_CurrencyCode = @CurrencyCode
                        AND CashTransTypeCode = @CashTransTypeCode";

                    var cashTransactions = await connection.QueryAsync<CashTransactionResponse>(
                        sql, 
                        new { 
                            CurrencyCode = currencyCode,
                            CashTransTypeCode = cashTransTypeCode
                        });
                    return cashTransactions.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving cash transactions by currency: {CurrencyCode} and type: {CashTransTypeCode}", currencyCode, cashTransTypeCode);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash transactions by currency: {CurrencyCode} and type: {CashTransTypeCode}", currencyCode, cashTransTypeCode);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir tarih aralığındaki kasa hareketlerini getirir
        /// </summary>
        public async Task<List<CashTransactionResponse>> GetCashTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM AllCashs WITH(NOLOCK)
                        WHERE DocumentDate BETWEEN @StartDate AND @EndDate";

                    var cashTransactions = await connection.QueryAsync<CashTransactionResponse>(
                        sql, 
                        new { 
                            StartDate = startDate,
                            EndDate = endDate
                        });
                    return cashTransactions.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving cash transactions by date range: {StartDate} - {EndDate}", startDate, endDate);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash transactions by date range: {StartDate} - {EndDate}", startDate, endDate);
                return new List<CashTransactionResponse>(); // Hata durumunda boş liste döndür
            }
        }
    }
}
