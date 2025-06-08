using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ErpMobile.Api.Models.Responses;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Data;

namespace ErpMobile.Api.Repositories.CashAccount
{
    public class CashAccountRepository : ICashAccountRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<CashAccountRepository> _logger;

        public CashAccountRepository(DapperContext context, ILogger<CashAccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<CashAccountResponse>> GetCashAccountsAsync()
        {
            _logger.LogInformation("CashAccountRepository.GetCashAccountsAsync() called");
            try
            {
                _logger.LogInformation("Preparing SQL query for cash accounts");
                var query = @"
                    SELECT * FROM (
                        SELECT 
                            CashAccountCode = cdCurrAcc.CurrAccCode,
                            CashAccountDescription = CurrAccDescription,
                            cdCurrAcc.CurrencyCode,
                            CurrencyDescription = ISNULL((
                                SELECT CurrencyDescription 
                                FROM cdCurrencyDesc WITH(NOLOCK) 
                                WHERE cdCurrencyDesc.CurrencyCode = cdCurrAcc.CurrencyCode 
                                AND cdCurrencyDesc.LangCode = N'TR'), SPACE(0)),
                            cdCurrAcc.CompanyCode,
                            cdCurrAcc.OfficeCode,
                            OfficeDescription = ISNULL((
                                SELECT OfficeDescription 
                                FROM cdOfficeDesc WITH(NOLOCK) 
                                WHERE cdOfficeDesc.OfficeCode = cdCurrAcc.OfficeCode 
                                AND cdOfficeDesc.LangCode = N'TR'), SPACE(0)),
                            StoreCode = ISNULL((
                                SELECT MAX(StoreCode) 
                                FROM prStoreCashAcc WITH(NOLOCK) 
                                WHERE CashCurrAccCode = cdCurrAcc.CurrAccCode), SPACE(0)),
                            cdCurrAcc.IsBlocked
                        FROM cdCurrAcc WITH(NOLOCK)
                        LEFT OUTER JOIN cdCurrAccDesc WITH(NOLOCK) 
                            ON cdCurrAccDesc.CurrAccTypeCode = cdCurrAcc.CurrAccTypeCode 
                            AND cdCurrAccDesc.CurrAccCode = cdCurrAcc.CurrAccCode 
                            AND cdCurrAccDesc.LangCode = N'TR'
                        WHERE cdCurrAcc.CurrAccTypeCode = 7
                        AND cdCurrAcc.CurrAccCode <> SPACE(0)
                    ) AS QueryTable";

                _logger.LogInformation("Executing SQL query via Dapper");
                using (var connection = _context.CreateConnection())
                {
                    var cashAccounts = await connection.QueryAsync<CashAccountResponse>(query);
                    _logger.LogInformation($"SQL query executed successfully. Retrieved {cashAccounts.Count()} cash accounts");
                    return cashAccounts.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash accounts from database");
                throw;
            }
        }
    }
}
