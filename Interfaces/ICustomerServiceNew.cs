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
        
        /// <summary>
        /// Müşteri adres bilgisi ekler
        /// </summary>
        /// <param name="request">Müşteri adres bilgisi ekleme isteği</param>
        /// <returns>Eklenen adres bilgisi</returns>
        Task<CustomerAddressResponse> AddCustomerAddressAsync(CustomerAddressRequest request);
        
        /// <summary>
        /// Müşteri iletişim bilgisi ekler
        /// </summary>
        /// <param name="request">Müşteri iletişim bilgisi ekleme isteği</param>
        /// <returns>Eklenen iletişim bilgisi</returns>
        Task<CustomerCommunicationResponse> AddCustomerCommunicationAsync(CustomerCommunicationRequest request);
        
        /// <summary>
        /// Müşteri kişi bilgisi ekler
        /// </summary>
        /// <param name="request">Müşteri kişi bilgisi ekleme isteği</param>
        /// <returns>Eklenen kişi bilgisi</returns>
        Task<CustomerContactResponse> AddCustomerContactAsync(CustomerContactCreateRequestNew request);
    }
}
