using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Müşteri iletişim işlemleri için servis arayüzü
    /// </summary>
    public interface ICustomerCommunicationService
    {
        /// <summary>
        /// Müşteri iletişim bilgilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri iletişim bilgileri listesi</returns>
        Task<List<CustomerCommunicationResponse>> GetCustomerCommunicationsAsync(string customerCode);
        
        /// <summary>
        /// Müşteri için iletişim bilgisi ekler
        /// </summary>
        /// <param name="request">Müşteri iletişim bilgisi ekleme isteği</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> AddCustomerCommunicationAsync(CustomerCommunicationCreateRequestNew request);
        
        /// <summary>
        /// Müşteri için iletişim bilgisi ekler (dahili kullanım)
        /// </summary>
        /// <param name="request">Müşteri iletişim bilgisi ekleme isteği</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> AddCustomerCommunicationInternalAsync(CustomerCommunicationCreateRequestNew request, Microsoft.Data.SqlClient.SqlConnection connection, Microsoft.Data.SqlClient.SqlTransaction transaction);
        
        /// <summary>
        /// Varsayılan iletişim bilgisini ayarlar
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="communicationId">İletişim bilgisi ID</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> SetDefaultCommunicationAsync(string customerCode, Guid communicationId, Microsoft.Data.SqlClient.SqlConnection connection, Microsoft.Data.SqlClient.SqlTransaction transaction);
    }
}
