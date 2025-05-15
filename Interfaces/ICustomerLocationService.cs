using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Models.Requests;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Müşteri konum işlemleri için servis arayüzü
    /// </summary>
    public interface ICustomerLocationService
    {
        /// <summary>
        /// Bölgeleri getirir
        /// </summary>
        Task<List<RegionResponse>> GetRegionsAsync();
        
        /// <summary>
        /// Bölgeleri getirir (States)
        /// </summary>
        /// <param name="countryCode">Ülke kodu (isteğe bağlı)</param>
        Task<List<StateResponse>> GetStatesAsync(string countryCode = null);
        
        /// <summary>
        /// Şehirleri getirir (Cities)
        /// </summary>
        Task<List<CityResponse>> GetCitiesAsync();
        
        /// <summary>
        /// İle göre şehirleri getirir
        /// </summary>
        Task<List<CityResponse>> GetCitiesByStateAsync(string stateCode);
        
        /// <summary>
        /// Bölgeye göre şehirleri getirir
        /// </summary>
        Task<List<CityResponse>> GetCitiesByRegionAsync(string regionCode);
        
        /// <summary>
        /// Şehre göre ilçeleri getirir
        /// </summary>
        Task<List<DistrictResponse>> GetDistrictsByCityAsync(string cityCode);
        
        /// <summary>
        /// İlçeleri getirir (Districts)
        /// </summary>
        Task<List<DistrictResponse>> GetAllDistrictsAsync();
        
        /// <summary>
        /// Vergi dairelerini getirir
        /// </summary>
        Task<List<TaxOfficeResponse>> GetTaxOfficesAsync();
        
        /// <summary>
        /// Banka hesaplarını getirir
        /// </summary>
        Task<List<BankAccountResponse>> GetBankAccountsAsync(string customerCode = null);
    }
}
