using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Contact;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Customer;

namespace ErpMobile.Api.Services
{
    /// <summary>
    /// Geçici olarak eksik metotları sağlayan stub servis sınıfı
    /// </summary>
    public class CustomerStubService
    {
        private readonly ILogger<CustomerStubService> _logger;

        public CustomerStubService(ILogger<CustomerStubService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Müşteri adres bilgilerini getirir (Stub)
        /// </summary>
        public async Task<List<AddressResponse>> GetCustomerAddressesAsync(string customerCode)
        {
            _logger.LogInformation("GetCustomerAddressesAsync çağrıldı: {CustomerCode}", customerCode);
            return new List<AddressResponse>();
        }

        /// <summary>
        /// Müşteri iletişim bilgilerini getirir (Stub)
        /// </summary>
        public async Task<List<CommunicationResponse>> GetCustomerCommunicationsAsync(string customerCode)
        {
            _logger.LogInformation("GetCustomerCommunicationsAsync çağrıldı: {CustomerCode}", customerCode);
            return new List<CommunicationResponse>();
        }

        /// <summary>
        /// Adres tiplerini getirir (Stub)
        /// </summary>
        public async Task<List<AddressTypeResponse>> GetAddressTypesAsync()
        {
            _logger.LogInformation("GetAddressTypesAsync çağrıldı");
            return new List<AddressTypeResponse>();
        }

        /// <summary>
        /// Adres tipini koda göre getirir (Stub)
        /// </summary>
        public async Task<AddressTypeResponse> GetAddressTypeByCodeAsync(string code)
        {
            _logger.LogInformation("GetAddressTypeByCodeAsync çağrıldı: {Code}", code);
            return new AddressTypeResponse();
        }

        /// <summary>
        /// Adresleri getirir (Stub)
        /// </summary>
        public async Task<List<AddressResponse>> GetAddressesAsync(string customerCode)
        {
            _logger.LogInformation("GetAddressesAsync çağrıldı: {CustomerCode}", customerCode);
            return new List<AddressResponse>();
        }

        /// <summary>
        /// Adresi ID'ye göre getirir (Stub)
        /// </summary>
        public async Task<AddressResponse> GetAddressByIdAsync(string customerCode, string addressId)
        {
            _logger.LogInformation("GetAddressByIdAsync çağrıldı: {CustomerCode}, {AddressId}", customerCode, addressId);
            return new AddressResponse();
        }

        /// <summary>
        /// Adres oluşturur (Stub)
        /// </summary>
        public async Task<AddressResponse> CreateAddressAsync(string customerCode, AddressCreateRequest request)
        {
            _logger.LogInformation("CreateAddressAsync çağrıldı: {CustomerCode}", customerCode);
            return new AddressResponse();
        }

        /// <summary>
        /// Kontakları getirir (Stub)
        /// </summary>
        public async Task<List<ErpMobile.Api.Models.Responses.ContactResponse>> GetContactsAsync(string customerCode)
        {
            _logger.LogInformation("GetContactsAsync çağrıldı: {CustomerCode}", customerCode);
            return new List<ErpMobile.Api.Models.Responses.ContactResponse>();
        }

        /// <summary>
        /// Kontağı ID'ye göre getirir (Stub)
        /// </summary>
        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> GetContactByIdAsync(string customerCode, string contactId)
        {
            _logger.LogInformation("GetContactByIdAsync çağrıldı: {CustomerCode}, {ContactId}", customerCode, contactId);
            return new ErpMobile.Api.Models.Responses.ContactResponse();
        }

        /// <summary>
        /// Kontak oluşturur (Stub)
        /// </summary>
        public async Task<ErpMobile.Api.Models.Responses.ContactResponse> CreateContactAsync(string customerCode, ErpMobile.Api.Models.Contact.ContactCreateRequest request)
        {
            _logger.LogInformation("CreateContactAsync çağrıldı: {CustomerCode}", customerCode);
            return new ErpMobile.Api.Models.Responses.ContactResponse();
        }

        /// <summary>
        /// Kontak tiplerini getirir (Stub)
        /// </summary>
        public async Task<List<ContactTypeResponse>> GetContactTypesAsync()
        {
            _logger.LogInformation("GetContactTypesAsync çağrıldı");
            return new List<ContactTypeResponse>();
        }

        /// <summary>
        /// Kontak tipini koda göre getirir (Stub)
        /// </summary>
        public async Task<ContactTypeResponse> GetContactTypeByCodeAsync(string code)
        {
            _logger.LogInformation("GetContactTypeByCodeAsync çağrıldı: {Code}", code);
            return new ContactTypeResponse();
        }
    }
}
