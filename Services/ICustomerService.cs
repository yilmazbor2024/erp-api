using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.Results;

namespace Api.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerListDto>> GetCustomerList();
        Task<CustomerListDto> GetCustomerById(string id);
        Task<IEnumerable<CustomerTypeResult>> GetCustomerTypes(string langCode);
        Task<IEnumerable<CustomerDiscountGrResult>> GetCustomerDiscountGroups(string langCode);
        Task<IEnumerable<CustomerPaymentPlanGrResult>> GetCustomerPaymentPlanGroups(string langCode);
    }
} 