using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Repositories.CashAccount;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Services.CashAccount
{
    public class CashAccountService : ICashAccountService
    {
        private readonly ICashAccountRepository _cashAccountRepository;
        private readonly ILogger<CashAccountService> _logger;

        public CashAccountService(ICashAccountRepository cashAccountRepository, ILogger<CashAccountService> logger)
        {
            _cashAccountRepository = cashAccountRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CashAccountResponse>> GetCashAccountsAsync()
        {
            _logger.LogInformation("CashAccountService.GetCashAccountsAsync() called");
            try
            {
                _logger.LogInformation("Calling repository to fetch cash accounts");
                var result = await _cashAccountRepository.GetCashAccountsAsync();
                _logger.LogInformation($"Repository returned {result?.Count() ?? 0} cash accounts");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash accounts from repository");
                throw;
            }
        }
    }
}
