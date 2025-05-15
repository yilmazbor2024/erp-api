using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Müşteri adres işlemleri için servis arayüzü
    /// </summary>
    public interface ICustomerAddressService
    {
        /// <summary>
        /// Müşteri adreslerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri adres listesi</returns>
        Task<List<CustomerAddressResponse>> GetCustomerAddressesAsync(string customerCode);
        
        /// <summary>
        /// Adres tiplerini getirir
        /// </summary>
        /// <returns>Adres tipleri listesi</returns>
        Task<List<AddressTypeResponse>> GetAddressTypesAsync();
        
        /// <summary>
        /// Adres tipini kod ile getirir
        /// </summary>
        /// <param name="code">Adres tipi kodu</param>
        /// <returns>Adres tipi</returns>
        Task<AddressTypeResponse> GetAddressTypeByCodeAsync(string code);
        
        /// <summary>
        /// Adres tipi oluşturur
        /// </summary>
        /// <param name="request">Adres tipi oluşturma isteği</param>
        /// <returns>Oluşturulan adres tipi</returns>
        Task<AddressTypeResponse> CreateAddressTypeAsync(AddressTypeCreateRequest request);
        
        /// <summary>
        /// Adres tipi günceller
        /// </summary>
        /// <param name="code">Adres tipi kodu</param>
        /// <param name="request">Adres tipi güncelleme isteği</param>
        /// <returns>Güncellenen adres tipi</returns>
        Task<AddressTypeResponse> UpdateAddressTypeAsync(string code, AddressTypeUpdateRequest request);
        
        /// <summary>
        /// Adres tipi siler
        /// </summary>
        /// <param name="code">Adres tipi kodu</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> DeleteAddressTypeAsync(string code);
        
        /// <summary>
        /// Müşteri adreslerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Adres listesi</returns>
        Task<List<AddressResponse>> GetAddressesAsync(string customerCode);
        
        /// <summary>
        /// Müşteri adresini ID ile getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="addressId">Adres ID</param>
        /// <returns>Adres</returns>
        Task<AddressResponse> GetAddressByIdAsync(string customerCode, string addressId);
        
        /// <summary>
        /// Müşteri adresi oluşturur
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="request">Adres oluşturma isteği</param>
        /// <returns>Oluşturulan adres</returns>
        Task<AddressResponse> CreateAddressAsync(string customerCode, AddressCreateRequest request);
        
        /// <summary>
        /// Müşteri adresi günceller
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="addressTypeCode">Adres tipi kodu</param>
        /// <param name="request">Adres güncelleme isteği</param>
        /// <returns>Güncellenen adres</returns>
        Task<AddressResponse> UpdateAddressAsync(string customerCode, string addressTypeCode, AddressUpdateRequest request);
        
        /// <summary>
        /// Müşteri adresi siler
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="addressTypeCode">Adres tipi kodu</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> DeleteAddressAsync(string customerCode, string addressTypeCode);
        
        /// <summary>
        /// Müşteri için adres ekler
        /// </summary>
        /// <param name="request">Müşteri adres ekleme isteği</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> AddCustomerAddressAsync(CustomerAddressCreateRequestNew request);
        
        /// <summary>
        /// Müşteri için adres ekler (dahili kullanım)
        /// </summary>
        /// <param name="request">Müşteri adres ekleme isteği</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> AddCustomerAddressInternalAsync(CustomerAddressCreateRequestNew request, Microsoft.Data.SqlClient.SqlConnection connection, Microsoft.Data.SqlClient.SqlTransaction transaction);
        
        /// <summary>
        /// Varsayılan adresi ayarlar
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <param name="addressId">Adres ID</param>
        /// <param name="connection">Veritabanı bağlantısı</param>
        /// <param name="transaction">Veritabanı işlemi</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> SetDefaultAddressAsync(string customerCode, Guid addressId, Microsoft.Data.SqlClient.SqlConnection connection, Microsoft.Data.SqlClient.SqlTransaction transaction);
    }
}
