using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models;

namespace ErpMobile.Api.Interfaces
{
    public interface ICustomerDebtService
    {
        /// <summary>
        /// Tüm müşteri borçlarını getirir
        /// </summary>
        Task<List<CustomerDebtResponse>> GetAllCustomerDebtsAsync(string langCode = "TR");

        /// <summary>
        /// Belirli bir müşterinin borçlarını getirir
        /// </summary>
        Task<List<CustomerDebtResponse>> GetCustomerDebtsByCustomerCodeAsync(string customerCode, string langCode = "TR");

        /// <summary>
        /// Belirli bir para birimine ait müşteri borçlarını getirir
        /// </summary>
        Task<List<CustomerDebtResponse>> GetCustomerDebtsByCurrencyAsync(string currencyCode, string langCode = "TR");

        /// <summary>
        /// Vadesi geçmiş müşteri borçlarını getirir
        /// </summary>
        Task<List<CustomerDebtResponse>> GetOverdueCustomerDebtsAsync(string langCode = "TR");

        /// <summary>
        /// Belirli bir müşterinin borç özetini getirir
        /// </summary>
        Task<CustomerDebtSummaryResponse> GetCustomerDebtSummaryAsync(string customerCode, string langCode = "TR");

        /// <summary>
        /// Tüm müşterilerin borç özetlerini getirir
        /// </summary>
        Task<List<CustomerDebtSummaryResponse>> GetAllCustomerDebtSummariesAsync(string langCode = "TR");
    }
}
