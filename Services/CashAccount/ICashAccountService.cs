using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Services.CashAccount
{
    public interface ICashAccountService
    {
        Task<IEnumerable<CashAccountResponse>> GetCashAccountsAsync();
    }
}
