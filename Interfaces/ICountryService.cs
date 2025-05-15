using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Ülke verilerini getirmek için servis arayüzü
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Ülke listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Ülke listesi</returns>
        Task<List<CountryResponse>> GetCountriesAsync(string langCode);
    }
}
