using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    public class CustomerCreditService : ICustomerCreditService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerCreditService> _logger;

        public CustomerCreditService(IConfiguration configuration, ILogger<CustomerCreditService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Tüm müşteri alacaklarını getirir
        /// </summary>
        public async Task<List<CustomerCreditResponse>> GetAllCustomerCreditsAsync(string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccCredits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        ORDER BY DocumentDate DESC";

                    var customerCredits = await connection.QueryAsync<CustomerCreditResponse>(sql);
                    return customerCredits.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving all customer credits");
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all customer credits");
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir müşterinin alacaklarını getirir
        /// </summary>
        public async Task<List<CustomerCreditResponse>> GetCustomerCreditsByCustomerCodeAsync(string customerCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccCredits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND CurrAccCode = @CustomerCode
                        ORDER BY DocumentDate DESC";

                    var customerCredits = await connection.QueryAsync<CustomerCreditResponse>(sql, new { CustomerCode = customerCode });
                    return customerCredits.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving customer credits for customer: {CustomerCode}", customerCode);
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving customer credits for customer: {CustomerCode}", customerCode);
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir para birimine ait müşteri alacaklarını getirir
        /// </summary>
        public async Task<List<CustomerCreditResponse>> GetCustomerCreditsByCurrencyAsync(string currencyCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccCredits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND Doc_CurrencyCode = @CurrencyCode
                        ORDER BY DocumentDate DESC";

                    var customerCredits = await connection.QueryAsync<CustomerCreditResponse>(sql, new { CurrencyCode = currencyCode });
                    return customerCredits.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving customer credits for currency: {CurrencyCode}", currencyCode);
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving customer credits for currency: {CurrencyCode}", currencyCode);
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir ödeme tipine ait müşteri alacaklarını getirir
        /// </summary>
        public async Task<List<CustomerCreditResponse>> GetCustomerCreditsByPaymentTypeAsync(int paymentTypeCode, string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccCredits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND PaymentTypeCode = @PaymentTypeCode
                        ORDER BY DocumentDate DESC";

                    var customerCredits = await connection.QueryAsync<CustomerCreditResponse>(sql, new { PaymentTypeCode = paymentTypeCode });
                    return customerCredits.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving customer credits for payment type: {PaymentTypeCode}", paymentTypeCode);
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving customer credits for payment type: {PaymentTypeCode}", paymentTypeCode);
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Vadesi geçmiş müşteri alacaklarını getirir
        /// </summary>
        public async Task<List<CustomerCreditResponse>> GetOverdueCustomerCreditsAsync(string langCode = "TR")
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    var sql = @"
                        SELECT * FROM CurrAccCredits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND DueDate < GETDATE()
                        ORDER BY DueDate ASC";

                    var customerCredits = await connection.QueryAsync<CustomerCreditResponse>(sql);
                    return customerCredits.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving overdue customer credits");
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving overdue customer credits");
                return new List<CustomerCreditResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Belirli bir müşterinin alacak özetini getirir
        /// </summary>
        public async Task<List<CustomerCreditSummaryResponse>> GetCustomerCreditSummaryAsync(string customerCode, string langCode = "TR")
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
                            TotalCredit = SUM(Loc_Amount),
                            TotalBalance = SUM(Loc_Balance),
                            CreditCount = COUNT(*),
                            OldestCreditDate = MIN(DocumentDate),
                            LatestCreditDate = MAX(DocumentDate),
                            PaymentTypeCode,
                            PaymentTypeDescription = CASE PaymentTypeCode 
                                WHEN 1 THEN N'Nakit' 
                                WHEN 2 THEN N'Kredi Kartı' 
                                WHEN 3 THEN N'Hediye Kartı' 
                                WHEN 4 THEN N'Banka' 
                                WHEN 5 THEN N'Borç' 
                                WHEN 10 THEN N'Müşteri Çeki' 
                                WHEN 20 THEN N'Kendi Çekimiz' 
                                WHEN 40 THEN N'Müşteri Senedi' 
                                WHEN 50 THEN N'Kendi Senedimiz' 
                                WHEN 70 THEN N'Diğer Ödeme' 
                                ELSE N'Bilinmeyen'
                            END
                        FROM CurrAccCredits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        AND CurrAccCode = @CustomerCode
                        GROUP BY Loc_CurrencyCode, PaymentTypeCode
                        ORDER BY PaymentTypeCode";

                    var customerCreditSummaries = await connection.QueryAsync<CustomerCreditSummaryResponse>(
                        sql, 
                        new { 
                            CustomerCode = customerCode,
                            LangCode = langCode
                        });
                    
                    return customerCreditSummaries.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving credit summary for customer: {CustomerCode}", customerCode);
                return new List<CustomerCreditSummaryResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving credit summary for customer: {CustomerCode}", customerCode);
                return new List<CustomerCreditSummaryResponse>(); // Hata durumunda boş liste döndür
            }
        }

        /// <summary>
        /// Tüm müşterilerin alacak özetlerini getirir
        /// </summary>
        public async Task<List<CustomerCreditSummaryResponse>> GetAllCustomerCreditSummariesAsync(string langCode = "TR")
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
                                                 WHERE CurrAccCode = CurrAccCredits.CurrAccCode AND CurrAccTypeCode = 3 AND LangCode = @LangCode),
                            CurrencyCode = Loc_CurrencyCode,
                            TotalCredit = SUM(Loc_Amount),
                            TotalBalance = SUM(Loc_Balance),
                            CreditCount = COUNT(*),
                            OldestCreditDate = MIN(DocumentDate),
                            LatestCreditDate = MAX(DocumentDate),
                            PaymentTypeCode,
                            PaymentTypeDescription = CASE PaymentTypeCode 
                                WHEN 1 THEN N'Nakit' 
                                WHEN 2 THEN N'Kredi Kartı' 
                                WHEN 3 THEN N'Hediye Kartı' 
                                WHEN 4 THEN N'Banka' 
                                WHEN 5 THEN N'Borç' 
                                WHEN 10 THEN N'Müşteri Çeki' 
                                WHEN 20 THEN N'Kendi Çekimiz' 
                                WHEN 40 THEN N'Müşteri Senedi' 
                                WHEN 50 THEN N'Kendi Senedimiz' 
                                WHEN 70 THEN N'Diğer Ödeme' 
                                ELSE N'Bilinmeyen'
                            END
                        FROM CurrAccCredits WITH(NOLOCK)
                        WHERE CurrAccTypeCode = 3 -- Müşteri tipi
                        GROUP BY CurrAccCode, Loc_CurrencyCode, PaymentTypeCode
                        ORDER BY CurrAccCode, PaymentTypeCode";

                    var customerCreditSummaries = await connection.QueryAsync<CustomerCreditSummaryResponse>(sql, new { LangCode = langCode });
                    return customerCreditSummaries.ToList();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred while retrieving all customer credit summaries");
                return new List<CustomerCreditSummaryResponse>(); // Hata durumunda boş liste döndür
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all customer credit summaries");
                return new List<CustomerCreditSummaryResponse>(); // Hata durumunda boş liste döndür
            }
        }
    }
}
