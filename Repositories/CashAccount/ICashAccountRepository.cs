using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Repositories.CashAccount
{
    public interface ICashAccountRepository
    {
        Task<IEnumerable<CashAccountResponse>> GetCashAccountsAsync();
    }
}
