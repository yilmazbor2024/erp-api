using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services.CashAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashAccountController : ControllerBase
    {
        private readonly ICashAccountService _cashAccountService;
        private readonly ILogger<CashAccountController> _logger;

        public CashAccountController(ICashAccountService cashAccountService, ILogger<CashAccountController> logger)
        {
            _cashAccountService = cashAccountService;
            _logger = logger;
        }

        /// <summary>
        /// Kasa hesaplarını listeler
        /// </summary>
        /// <returns>Kasa hesapları listesi</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CashAccountResponse>>> GetCashAccounts()
        {
            _logger.LogInformation("API Request received: GET /api/CashAccount");
            try
            {
                _logger.LogInformation("Calling CashAccountService.GetCashAccountsAsync()");
                var cashAccounts = await _cashAccountService.GetCashAccountsAsync();
                _logger.LogInformation($"Retrieved {cashAccounts?.Count() ?? 0} cash accounts successfully");
                return Ok(cashAccounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash accounts");
                return StatusCode(500, "Internal server error occurred while retrieving cash accounts");
            }
        }
    }
}
