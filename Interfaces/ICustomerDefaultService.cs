using System.Threading.Tasks;
using ErpMobile.Api.Models.Customer;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    /// <summary>
    /// Müşteri varsayılan bilgileri servisi
    /// </summary>
    public interface ICustomerDefaultService
    {
        /// <summary>
        /// Müşteri varsayılan adres ve iletişim bilgilerini getirir
        /// </summary>
        /// <param name="customerCode">Müşteri kodu</param>
        /// <returns>Müşteri varsayılan bilgileri</returns>
        Task<CustomerDefaultAddressResponse> GetCustomerDefaultsAsync(string customerCode);
    }
}
