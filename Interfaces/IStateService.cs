using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// State (Bölge) verilerini sağlamak için servis arayüzü
    /// </summary>
    public interface IStateService
    {
        /// <summary>
        /// State listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>State listesi</returns>
        Task<List<StateResponse>> GetStatesAsync(string langCode);
    }
}
