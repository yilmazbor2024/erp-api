using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Müşteri kişi işlemleri için servis arayüzü
    /// </summary>
    public interface ICustomerContactService
    {
        /// <summary>
        /// Müşteri kişilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri kişi listesi</returns>
        Task<List<CustomerContactResponse>> GetCustomerContactsAsync(string customerCode);
        
        /// <summary>
        /// Kişi tiplerini getirir
        /// </summary>
        /// <returns>Kişi tipleri listesi</returns>
        Task<List<ContactTypeResponse>> GetContactTypesAsync();
        
        /// <summary>
        /// Kişi tipini kod ile getirir
        /// </summary>
        /// <param name="code">Kişi tipi kodu</param>
        /// <returns>Kişi tipi</returns>
        Task<ContactTypeResponse> GetContactTypeByCodeAsync(string code);
        
        /// <summary>
        /// Müşteri kişilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Kişi listesi</returns>
        Task<List<ErpMobile.Api.Models.Responses.ContactResponse>> GetContactsAsync(string customerCode);
        
        /// <summary>
        /// Müşteri kişisini ID ile getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="contactId">Kişi ID</param>
        /// <returns>Kişi</returns>
        Task<ErpMobile.Api.Models.Responses.ContactResponse> GetContactByIdAsync(string customerCode, string contactId);
        
        /// <summary>
        /// Müşteri kişisi oluşturur
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="request">Kişi oluşturma isteği</param>
        /// <returns>Oluşturulan kişi</returns>
        Task<ErpMobile.Api.Models.Responses.ContactResponse> CreateContactAsync(string customerCode, ContactCreateRequest request);
        
        /// <summary>
        /// Müşteri kişisi günceller
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="contactTypeCode">Kişi tipi kodu</param>
        /// <param name="request">Kişi güncelleme isteği</param>
        /// <returns>Güncellenen kişi</returns>
        Task<ErpMobile.Api.Models.Responses.ContactResponse> UpdateContactAsync(string customerCode, string contactTypeCode, ContactUpdateRequest request);
        
        /// <summary>
        /// Müşteri kişisi siler
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="contactTypeCode">Kişi tipi kodu</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> DeleteContactAsync(string customerCode, string contactTypeCode);
        
        /// <summary>
        /// Müşteri için kişi ekler
        /// </summary>
        /// <param name="request">Müşteri kişi ekleme isteği</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> AddCustomerContactAsync(CustomerContactCreateRequestNew request);
        
        /// <summary>
        /// Müşteri için kişi ekler (dahili kullanım)
        /// </summary>
        /// <param name="request">Müşteri kişi ekleme isteği</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> AddCustomerContactInternalAsync(CustomerContactCreateRequestNew request, Microsoft.Data.SqlClient.SqlConnection connection, Microsoft.Data.SqlClient.SqlTransaction transaction);
        
        /// <summary>
        /// Varsayılan kişiyi ayarlar
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="contactId">Kişi ID</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> SetDefaultContactAsync(string customerCode, Guid contactId, Microsoft.Data.SqlClient.SqlConnection connection, Microsoft.Data.SqlClient.SqlTransaction transaction);
    }
}
