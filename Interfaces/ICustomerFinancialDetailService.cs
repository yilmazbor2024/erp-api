using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    public interface ICustomerFinancialDetailService
    {
        Task<CustomerFinancialDetailResponse> GetCustomerFinancialDetailsByCodeAsync(string customerCode);
    }
}
