using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Common;

namespace ErpMobile.Api.Interfaces
{
    public interface ICustomerFinancialService
    {
        /// <summary>
        /// Müşteri kredi bilgilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri kredi bilgileri</returns>
        Task<CustomerCreditInfoResponse> GetCustomerCreditInfoAsync(string customerCode);

        /// <summary>
        /// Müşteri finansal bilgilerini günceller
        /// </summary>
        /// <param name="request">Güncelleme isteği</param>
        /// <returns>Güncelleme sonucu</returns>
        Task<CustomerFinancialUpdateResponse> UpdateCustomerFinancialAsync(CustomerFinancialUpdateRequest request);

        /// <summary>
        /// Müşteri işlemlerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Müşteri işlemleri listesi</returns>
        Task<List<CustomerTransactionResponse>> GetCustomerTransactionsAsync(string customerCode, DateTime? startDate, DateTime? endDate);
        
        /// <summary>
        /// Müşteri işlemlerini sayfalanmış şekilde getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa boyutu</param>
        /// <returns>Sayfalanmış müşteri işlemleri</returns>
        Task<PagedResponse<CustomerTransactionResponse>> GetCustomerTransactionsPagedAsync(string customerCode, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize);
    }
}
