using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services.CashTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashTransactionController : ControllerBase
    {
        private readonly ICashTransactionService _cashTransactionService;
        private readonly ILogger<CashTransactionController> _logger;

        public CashTransactionController(ICashTransactionService cashTransactionService, ILogger<CashTransactionController> logger)
        {
            _cashTransactionService = cashTransactionService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("transactions")]
        public async Task<ActionResult<IEnumerable<CashTransactionResponse>>> GetCashTransactions(
            [FromQuery] DateTime? startDate = null, 
            [FromQuery] DateTime? endDate = null, 
            [FromQuery] string cashAccountCode = null)
        {
            _logger.LogInformation("API Request received: GET /api/CashTransaction/transactions");
            
            try
            {
                var start = startDate ?? DateTime.Now.AddMonths(-1);
                var end = endDate ?? DateTime.Now;
                
                _logger.LogInformation("Calling CashTransactionService.GetCashTransactionsAsync() with startDate: {StartDate}, endDate: {EndDate}, cashAccountCode: {CashAccountCode}", 
                    start, end, cashAccountCode ?? "null");
                
                var cashTransactions = await _cashTransactionService.GetCashTransactionsAsync(start, end, cashAccountCode);
                _logger.LogInformation("Retrieved {Count} cash transactions successfully", cashTransactions?.Count() ?? 0);
                
                return Ok(cashTransactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash transactions");
                return StatusCode(500, "Internal server error occurred while retrieving cash transactions");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("summary")]
        public async Task<ActionResult<IEnumerable<CashSummaryResponse>>> GetCashSummary()
        {
            _logger.LogInformation("API Request received: GET /api/CashTransaction/summary");
            
            try
            {
                _logger.LogInformation("Calling CashTransactionService.GetCashSummaryAsync()");
                var cashSummaries = await _cashTransactionService.GetCashSummaryAsync();
                _logger.LogInformation("Retrieved {Count} cash summaries successfully", cashSummaries?.Count() ?? 0);
                
                return Ok(cashSummaries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash summaries");
                return StatusCode(500, "Internal server error occurred while retrieving cash summaries");
            }
        }
    }
}
