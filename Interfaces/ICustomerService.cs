using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Temel müşteri işlemleri için servis arayüzü
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Müşterinin var olup olmadığını kontrol eder
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri varsa true, yoksa false</returns>
        Task<bool> CustomerExistsAsync(string customerCode);

        /// <summary>
        /// Müşteri listesini getirir
        /// </summary>
        /// <param name="filter">Müşteri filtreleme parametreleri</param>
        /// <returns>Sayfalanmış müşteri listesi</returns>
        Task<PagedResponse<CustomerListResponse>> GetCustomerListAsync(CustomerFilterRequest filter);
        
        /// <summary>
        /// Müşteri veya tedarikçi koduna göre detayını getirir
        /// </summary>
        /// <param name="customerCode">Müşteri veya tedarikçi kodu</param>
        /// <param name="currAccTypeCode">Kayıt tipi kodu (1: Tedarikçi, 3: Müşteri)</param>
        /// <returns>Müşteri veya tedarikçi detayı</returns>
        Task<CustomerDetailResponse> GetCustomerByCodeAsync(string customerCode, int? currAccTypeCode = null);
        
        /// <summary>
        /// Müşteri ID'sine göre müşteri detayını getirir
        /// </summary>
        /// <param name="id">Müşteri ID</param>
        /// <returns>Müşteri detayı</returns>
        Task<CustomerDetailResponse> GetCustomerByIdAsync(string id);
        
        /// <summary>
        /// Yeni müşteri oluşturur
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        Task<CustomerResponse> CreateCustomerAsync(CustomerCreateRequest request);
        
        /// <summary>
        /// Yeni müşteri oluşturur (Geliştirilmiş versiyon)
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        Task<CustomerDetailResponse> CreateCustomerAsync(CustomerCreateRequestNew request);
        
        /// <summary>
        /// Müşteri bilgilerini günceller
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>İşlem başarılı ise true, değilse false</returns>
        Task<bool> UpdateCustomerAsync(CustomerUpdateRequest request);
        
        /// <summary>
        /// Müşteri bilgilerini günceller (Geliştirilmiş versiyon)
        /// </summary>
        /// <param name="request">Müşteri güncelleme isteği</param>
        /// <returns>Güncellenen müşteri bilgileri</returns>
        Task<CustomerUpdateResponseNew> UpdateCustomerAsync(CustomerUpdateRequestNew request);
        
        /// <summary>
        /// Müşteri finansal bilgilerini günceller
        /// </summary>
        /// <param name="request">Müşteri finansal bilgi güncelleme isteği</param>
        /// <returns>Güncelleme sonucu ve bilgileri</returns>
        Task<CustomerFinancialUpdateResponse> UpdateCustomerFinancialAsync(CustomerFinancialUpdateRequest request);
        
        /// <summary>
        /// Müşteri tiplerini getirir
        /// </summary>
        /// <returns>Müşteri tipleri listesi</returns>
        Task<List<CustomerTypeResponse>> GetCustomerTypesAsync();
        
        /// <summary>
        /// Müşteri indirim gruplarını getirir
        /// </summary>
        /// <returns>Müşteri indirim grupları listesi</returns>
        Task<List<CustomerDiscountGroupResponse>> GetCustomerDiscountGroupsAsync();
        
        /// <summary>
        /// Müşteri ödeme planı gruplarını getirir
        /// </summary>
        /// <returns>Müşteri ödeme planı grupları listesi</returns>
        Task<List<CustomerPaymentPlanGroupResponse>> GetCustomerPaymentPlanGroupsAsync();
        
        /// <summary>
        /// Bölgeleri getirir
        /// </summary>
        /// <returns>Bölge listesi</returns>
        Task<List<RegionResponse>> GetRegionsAsync();
        
        /// <summary>
        /// İlleri getirir
        /// </summary>
        /// <param name="countryCode">Ülke kodu</param>
        /// <returns>İl listesi</returns>
        Task<List<StateResponse>> GetStatesAsync(string countryCode = null);
        
        /// <summary>
        /// İlçeleri getirir
        /// </summary>
        /// <returns>İlçe listesi</returns>
        Task<List<CityResponse>> GetCitiesAsync();
        
        /// <summary>
        /// İle göre ilçeleri getirir
        /// </summary>
        /// <param name="stateCode">İl kodu</param>
        /// <returns>İlçe listesi</returns>
        Task<List<CityResponse>> GetCitiesByStateAsync(string stateCode);
        
        /// <summary>
        /// Bölgeye göre ilçeleri getirir
        /// </summary>
        /// <param name="regionCode">Bölge kodu</param>
        /// <returns>İlçe listesi</returns>
        Task<List<CityResponse>> GetCitiesByRegionAsync(string regionCode);
        
        /// <summary>
        /// İlçeye göre mahalleleri getirir
        /// </summary>
        /// <param name="cityCode">İlçe kodu</param>
        /// <returns>Mahalle listesi</returns>
        Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode);
        
        /// <summary>
        /// Tüm mahalleleri getirir
        /// </summary>
        /// <returns>Mahalle listesi</returns>
        Task<List<DistrictResponse>> GetAllDistrictsAsync();
        
        /// <summary>
        /// Vergi dairelerini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Vergi dairesi listesi</returns>
        Task<List<ErpMobile.Api.Models.TaxOfficeResponse>> GetTaxOfficesAsync(string langCode = "TR");
        
        // Banka hesapları artık ICustomerLocationService arayüzüne taşındı
    }
}