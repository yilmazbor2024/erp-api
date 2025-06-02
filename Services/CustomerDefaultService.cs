using System;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Customer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Müşteri varsayılan bilgileri servisi
    /// </summary>
    public class CustomerDefaultService : ICustomerDefaultService
    {
        private readonly ILogger<CustomerDefaultService> _logger;
        private readonly IConfiguration _configuration;

        public CustomerDefaultService(
            ILogger<CustomerDefaultService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Müşteri varsayılan adres ve iletişim bilgilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri varsayılan bilgileri</returns>
        public async Task<CustomerDefaultAddressResponse> GetCustomerDefaultsAsync(string customerCode)
        {
            _logger.LogInformation($"Müşteri varsayılan bilgileri getiriliyor: {customerCode}");

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ErpConnection")))
                {
                    await connection.OpenAsync();

                    // Müşteri tipini belirle (varsayılan olarak 3 - Müşteri)
                    int currAccTypeCode = 3;

                    // Müşteri varsayılan bilgilerini getir
                    var sql = @"
                    SELECT 
                        CurrAccCode = RTRIM(LTRIM(prCurrAccDefault.CurrAccCode)),
                        CurrAccTypeCode = prCurrAccDefault.CurrAccTypeCode,
                        PostalAddressID = prCurrAccDefault.PostalAddressID,
                        CommunicationID = prCurrAccDefault.CommunicationID,
                        ContactID = prCurrAccDefault.ContactID,
                        SubCurrAccID = prCurrAccDefault.SubCurrAccID,
                        EArchieveEMailCommunicationID = prCurrAccDefault.EArchieveEMailCommunicationID,
                        EArchieveMobileCommunicationID = prCurrAccDefault.EArchieveMobileCommunicationID,
                        OfficePhoneID = prCurrAccDefault.OfficePhoneID,
                        HomePhoneID = prCurrAccDefault.HomePhoneID,
                        BusinessMobileID = prCurrAccDefault.BusinessMobileID,
                        PersonalMobileID = prCurrAccDefault.PersonalMobileID,
                        ShippingAddressID = prCurrAccDefault.ShippingAddressID,
                        BillingAddressID = prCurrAccDefault.BillingAddressID,
                        GuidedSalesNotificationEmailID = prCurrAccDefault.GuidedSalesNotificationEmailID,
                        GuidedSalesNotificationPhoneID = prCurrAccDefault.GuidedSalesNotificationPhoneID
                    FROM prCurrAccDefault WITH(NOLOCK)
                    WHERE 
                        prCurrAccDefault.CurrAccCode = @CustomerCode
                        AND prCurrAccDefault.CurrAccTypeCode = @CurrAccTypeCode";

                    var customerDefaults = await connection.QueryFirstOrDefaultAsync<CustomerDefaultAddressResponse>(
                        sql,
                        new { CustomerCode = customerCode, CurrAccTypeCode = currAccTypeCode }
                    );

                    if (customerDefaults == null)
                    {
                        _logger.LogWarning($"Müşteri varsayılan bilgileri bulunamadı: {customerCode}");
                        return new CustomerDefaultAddressResponse
                        {
                            CurrAccCode = customerCode,
                            CurrAccTypeCode = currAccTypeCode
                        };
                    }

                    _logger.LogInformation($"Müşteri varsayılan bilgileri başarıyla getirildi: {customerCode}");
                    _logger.LogInformation($"Teslimat adresi ID: {customerDefaults.ShippingAddressID}");
                    _logger.LogInformation($"Fatura adresi ID: {customerDefaults.BillingAddressID}");

                    return customerDefaults;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Müşteri varsayılan bilgileri getirilirken hata oluştu: {customerCode}");
                throw;
            }
        }
    }
}
