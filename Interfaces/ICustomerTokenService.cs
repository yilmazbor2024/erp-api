using System.Threading.Tasks;
using ErpMobile.Api.Models.Customer;
using ErpMobile.Api.Models.Requests;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    public interface ICustomerTokenService
    {
        /// <summary>
        /// Token ile müşteri oluşturma
        /// </summary>
        /// <param name="request">Müşteri oluşturma isteği</param>
        /// <returns>Oluşturulan müşteri bilgileri</returns>
        Task<CustomerCreateResponse> CreateCustomerWithTokenAsync(CustomerCreateRequestNew request);
    }
}
