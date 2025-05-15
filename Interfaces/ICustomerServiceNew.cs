using System.Threading.Tasks;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Geliştirilmiş müşteri işlemleri için servis arayüzü
    /// </summary>
    public interface ICustomerServiceNew
    {
        /// <summary>
        /// Yeni müşteri oluşturur (Geliştirilmiş versiyon)
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        Task<CustomerCreateResponseNew> CreateCustomerAsync(CustomerCreateRequestNew request);
        
        /// <summary>
        /// Müşteri bilgilerini günceller (Geliştirilmiş versiyon)
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>Güncellenen müşteri bilgileri</returns>
        Task<CustomerUpdateResponseNew> UpdateCustomerAsync(CustomerUpdateRequestNew request);
    }
}
