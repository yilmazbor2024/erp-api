using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using erp_api.Models.Requests;
using erp_api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Yeni müşteri servisi için interface
    /// </summary>
    public interface ICustomerServiceNew
    {
        /// <summary>
        /// Yeni müşteri oluşturma metodu
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Müşteri oluşturma yanıtı</returns>
        Task<CustomerCreateResponseNew> CreateCustomerAsync(CustomerCreateRequestNew request);

        /// <summary>
        /// Müşteri güncelleme metodu
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>Müşteri güncelleme yanıtı</returns>
        Task<CustomerUpdateResponseNew> UpdateCustomerAsync(CustomerUpdateRequestNew request);
    }
}
