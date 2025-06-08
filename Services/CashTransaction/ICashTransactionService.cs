using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Services.CashTransaction
{
    public interface ICashTransactionService
    {
        Task<IEnumerable<CashTransactionResponse>> GetCashTransactionsAsync(DateTime startDate, DateTime endDate, string cashAccountCode = null);
        Task<IEnumerable<CashSummaryResponse>> GetCashSummaryAsync();
    }
}
