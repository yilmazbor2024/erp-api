using System.Collections.Generic;
using System.Threading.Tasks;
using erp_api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Para birimi servis arayüzü
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Para birimi listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Para birimi listesi</returns>
        Task<List<CurrencyResponse>> GetCurrenciesAsync(string langCode = "TR");

        /// <summary>
        /// Para birimi detayını getirir
        /// </summary>
        /// <param name="currencyCode">Para birimi kodu</param>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Para birimi detayı</returns>
        Task<CurrencyResponse> GetCurrencyByCodeAsync(string currencyCode, string langCode = "TR");
    }
} 