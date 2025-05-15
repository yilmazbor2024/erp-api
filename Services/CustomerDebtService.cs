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
    public class CustomerDebtService : ICustomerDebtService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerDebtService> _logger;

        public CustomerDebtService(IConfiguration configuration, ILogger<CustomerDebtService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Tüm müşteri borçlarını getirir
        /// </summary>
        public async Task<List<CustomerDebtResponse>> GetAllCustomerDebtsAsync(string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccDebits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        ORDER BY DocumentDate DESC";

                    var customerDebts = await connection.QueryAsync<CustomerDebtResponse>(sql);
                    return customerDebts.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving all customer debts");
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all customer debts");
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir müşterinin borçlarını getirir
        /// </summary>
        public async Task<List<CustomerDebtResponse>> GetCustomerDebtsByCustomerCodeAsync(string customerCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccDebits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND CurrAccCode = @CustomerCode
                        ORDER BY DocumentDate DESC";

                    var customerDebts = await connection.QueryAsync<CustomerDebtResponse>(sql, new { CustomerCode = customerCode });
                    return customerDebts.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving customer debts for customer: {CustomerCode}", customerCode);
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving customer debts for customer: {CustomerCode}", customerCode);
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir para birimine ait müşteri borçlarını getirir
        /// </summary>
        public async Task<List<CustomerDebtResponse>> GetCustomerDebtsByCurrencyAsync(string currencyCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccDebits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND Doc_CurrencyCode = @CurrencyCode
                        ORDER BY DocumentDate DESC";

                    var customerDebts = await connection.QueryAsync<CustomerDebtResponse>(sql, new { CurrencyCode = currencyCode });
                    return customerDebts.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving customer debts for currency: {CurrencyCode}", currencyCode);
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving customer debts for currency: {CurrencyCode}", currencyCode);
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Vadesi geçmiş müşteri borçlarını getirir
        /// </summary>
        public async Task<List<CustomerDebtResponse>> GetOverdueCustomerDebtsAsync(string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccDebits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND DueDate < GETDATE()
                        ORDER BY DueDate ASC";

                    var customerDebts = await connection.QueryAsync<CustomerDebtResponse>(sql);
                    return customerDebts.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving overdue customer debts");
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving overdue customer debts");
                return new List<CustomerDebtResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir müşterinin borç özetini getirir
        /// </summary>
        public async Task<CustomerDebtSummaryResponse> GetCustomerDebtSummaryAsync(string customerCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CurrAccCode = @CustomerCode,
                            CurrAccDescription = (SELECT TOP 1 CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) 
                                                 WHERE CurrAccCode = @CustomerCode AND CurrAccTypeCode = 3 AND LangCode = @LangCode),
                            CurrencyCode = Loc_CurrencyCode,
                            TotalDebt = SUM(Loc_Debit),
                            TotalBalance = SUM(Loc_Balance),
                            DebtCount = COUNT(*),
                            OldestDebtDate = MIN(DocumentDate),
                            LatestDebtDate = MAX(DocumentDate)
                        FROM CurrAccDebits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND CurrAccCode = @CustomerCode
                        GROUP BY Loc_CurrencyCode";

                    var customerDebtSummary = await connection.QueryFirstOrDefaultAsync<CustomerDebtSummaryResponse>(
                        sql, 
                        new { 
                            CustomerCode = customerCode,
                            LangCode = langCode
                        });
                    
                    return customerDebtSummary ?? new CustomerDebtSummaryResponse 
                    { 
                        CurrAccCode = customerCode,
                        TotalDebt = 0,
                        TotalBalance = 0,
                        DebtCount = 0
                    };
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving debt summary for customer: {CustomerCode}", customerCode);
                return new CustomerDebtSummaryResponse 
                { 
                    CurrAccCode = customerCode,
                    TotalDebt = 0,
                    TotalBalance = 0,
                    DebtCount = 0
                }; // Hata durumunda boş özet döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving debt summary for customer: {CustomerCode}", customerCode);
                return new CustomerDebtSummaryResponse 
                { 
                    CurrAccCode = customerCode,
                    TotalDebt = 0,
                    TotalBalance = 0,
                    DebtCount = 0
                }; // Hata durumunda boş özet döndür
            }
        }

        /// <summary>
        /// Tüm müşterilerin borç özetlerini getirir
        /// </summary>
        public async Task<List<CustomerDebtSummaryResponse>> GetAllCustomerDebtSummariesAsync(string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT 
                            CurrAccCode,
                            CurrAccDescription = (SELECT TOP 1 CurrAccDescription FROM cdCurrAccDesc WITH(NOLOCK) 
                                                 WHERE CurrAccCode = CurrAccDebits.CurrAccCode AND CurrAccTypeCode = 3 AND LangCode = @LangCode),
                            CurrencyCode = Loc_CurrencyCode,
                            TotalDebt = SUM(Loc_Debit),
                            TotalBalance = SUM(Loc_Balance),
                            DebtCount = COUNT(*),
                            OldestDebtDate = MIN(DocumentDate),
                            LatestDebtDate = MAX(DocumentDate)
                        FROM CurrAccDebits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        GROUP BY CurrAccCode, Loc_CurrencyCode
                        ORDER BY SUM(Loc_Balance) DESC";

                    var customerDebtSummaries = await connection.QueryAsync<CustomerDebtSummaryResponse>(sql, new { LangCode = langCode });
                    return customerDebtSummaries.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving all customer debt summaries");
                return new List<CustomerDebtSummaryResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all customer debt summaries");
                return new List<CustomerDebtSummaryResponse>(); // Hata durumunda boş liste döndür
            }
        }
    }
}
