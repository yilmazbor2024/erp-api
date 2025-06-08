using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Repositories.CashTransaction
{
    public interface ICashTransactionRepository
    {
        Task<IEnumerable<CashTransactionResponse>> GetCashTransactionsAsync(DateTime startDate, DateTime endDate, string cashAccountCode = null);
        Task<IEnumerable<CashSummaryResponse>> GetCashSummaryAsync();
    }
}
