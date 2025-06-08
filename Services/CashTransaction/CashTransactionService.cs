using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Repositories.CashTransaction;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services.CashTransaction
{
    public class CashTransactionService : ICashTransactionService
    {
        private readonly ICashTransactionRepository _cashTransactionRepository;
        private readonly ILogger<CashTransactionService> _logger;

        public CashTransactionService(ICashTransactionRepository cashTransactionRepository, ILogger<CashTransactionService> logger)
        {
            _cashTransactionRepository = cashTransactionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CashTransactionResponse>> GetCashTransactionsAsync(DateTime startDate, DateTime endDate, string cashAccountCode = null)
        {
            _logger.LogInformation("CashTransactionService.GetCashTransactionsAsync() called with startDate: {StartDate}, endDate: {EndDate}, cashAccountCode: {CashAccountCode}", 
                startDate, endDate, cashAccountCode ?? "null");
            
            try
            {
                _logger.LogInformation("Calling repository to fetch cash transactions");
                var result = await _cashTransactionRepository.GetCashTransactionsAsync(startDate, endDate, cashAccountCode);
                _logger.LogInformation("Repository returned {Count} cash transactions", result?.Count() ?? 0);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash transactions from repository");
                throw;
            }
        }

        public async Task<IEnumerable<CashSummaryResponse>> GetCashSummaryAsync()
        {
            _logger.LogInformation("CashTransactionService.GetCashSummaryAsync() called");
            
            try
            {
                _logger.LogInformation("Calling repository to fetch cash summaries");
                var result = await _cashTransactionRepository.GetCashSummaryAsync();
                _logger.LogInformation("Repository returned {Count} cash summaries", result?.Count() ?? 0);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash summaries from repository");
                throw;
            }
        }
    }
}
