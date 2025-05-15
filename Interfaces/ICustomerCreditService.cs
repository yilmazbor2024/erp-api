using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models;

namespace ErpMobile.Api.Interfaces
{
    public interface ICustomerCreditService
    {
        /// <summary>
        /// Tüm müşteri alacaklarını getirir
        /// </summary>
        Task<List<CustomerCreditResponse>> GetAllCustomerCreditsAsync(string langCode = "TR");

        /// <summary>
        /// Belirli bir müşterinin alacaklarını getirir
        /// </summary>
        Task<List<CustomerCreditResponse>> GetCustomerCreditsByCustomerCodeAsync(string customerCode, string langCode = "TR");

        /// <summary>
        /// Belirli bir para birimine ait müşteri alacaklarını getirir
        /// </summary>
        Task<List<CustomerCreditResponse>> GetCustomerCreditsByCurrencyAsync(string currencyCode, string langCode = "TR");

        /// <summary>
        /// Belirli bir ödeme tipine ait müşteri alacaklarını getirir
        /// </summary>
        Task<List<CustomerCreditResponse>> GetCustomerCreditsByPaymentTypeAsync(int paymentTypeCode, string langCode = "TR");

        /// <summary>
        /// Vadesi geçmiş müşteri alacaklarını getirir
        /// </summary>
        Task<List<CustomerCreditResponse>> GetOverdueCustomerCreditsAsync(string langCode = "TR");

        /// <summary>
        /// Belirli bir müşterinin alacak özetini getirir
        /// </summary>
        Task<List<CustomerCreditSummaryResponse>> GetCustomerCreditSummaryAsync(string customerCode, string langCode = "TR");

        /// <summary>
        /// Tüm müşterilerin alacak özetlerini getirir
        /// </summary>
        Task<List<CustomerCreditSummaryResponse>> GetAllCustomerCreditSummariesAsync(string langCode = "TR");
    }
}
