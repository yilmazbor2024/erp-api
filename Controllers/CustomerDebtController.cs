using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerDebtController : ControllerBase
    {
        private readonly ICustomerDebtService _customerDebtService;
        private readonly ILogger<CustomerDebtController> _logger;

        public CustomerDebtController(ICustomerDebtService customerDebtService, ILogger<CustomerDebtController> logger)
        {
            _customerDebtService = customerDebtService;
            _logger = logger;
        }

        /// <summary>
        /// Tüm müşteri borçlarını getirir
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<CustomerDebtResponse>>> GetAllCustomerDebts([FromQuery] string langCode = "TR")
        {
            try
            {
                var customerDebts = await _customerDebtService.GetAllCustomerDebtsAsync(langCode);
                return Ok(new ApiResponse<List<CustomerDebtResponse>>
                {
                    Success = true,
                    Message = "Müşteri borçları başarıyla getirildi",
                    Data = customerDebts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri borçları alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Müşteri borçları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir müşterinin borçlarını getirir
        /// </summary>
        [HttpGet("customer/{customerCode}")]
        public async Task<ActionResult<List<CustomerDebtResponse>>> GetCustomerDebtsByCustomerCode(string customerCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var customerDebts = await _customerDebtService.GetCustomerDebtsByCustomerCodeAsync(customerCode, langCode);
                return Ok(new ApiResponse<List<CustomerDebtResponse>>
                {
                    Success = true,
                    Message = $"{customerCode} kodlu müşterinin borçları başarıyla getirildi",
                    Data = customerDebts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CustomerCode} kodlu müşterinin borçları alınırken hata oluştu", customerCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{customerCode} kodlu müşterinin borçları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir para birimine ait müşteri borçlarını getirir
        /// </summary>
        [HttpGet("currency/{currencyCode}")]
        public async Task<ActionResult<List<CustomerDebtResponse>>> GetCustomerDebtsByCurrency(string currencyCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var customerDebts = await _customerDebtService.GetCustomerDebtsByCurrencyAsync(currencyCode, langCode);
                return Ok(new ApiResponse<List<CustomerDebtResponse>>
                {
                    Success = true,
                    Message = $"{currencyCode} para birimine ait müşteri borçları başarıyla getirildi",
                    Data = customerDebts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CurrencyCode} para birimine ait müşteri borçları alınırken hata oluştu", currencyCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{currencyCode} para birimine ait müşteri borçları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Vadesi geçmiş müşteri borçlarını getirir
        /// </summary>
        [HttpGet("overdue")]
        public async Task<ActionResult<List<CustomerDebtResponse>>> GetOverdueCustomerDebts([FromQuery] string langCode = "TR")
        {
            try
            {
                var customerDebts = await _customerDebtService.GetOverdueCustomerDebtsAsync(langCode);
                return Ok(new ApiResponse<List<CustomerDebtResponse>>
                {
                    Success = true,
                    Message = "Vadesi geçmiş müşteri borçları başarıyla getirildi",
                    Data = customerDebts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Vadesi geçmiş müşteri borçları alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Vadesi geçmiş müşteri borçları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir müşterinin borç özetini getirir
        /// </summary>
        [HttpGet("summary/customer/{customerCode}")]
        public async Task<ActionResult<CustomerDebtSummaryResponse>> GetCustomerDebtSummary(string customerCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var customerDebtSummary = await _customerDebtService.GetCustomerDebtSummaryAsync(customerCode, langCode);
                return Ok(new ApiResponse<CustomerDebtSummaryResponse>
                {
                    Success = true,
                    Message = $"{customerCode} kodlu müşterinin borç özeti başarıyla getirildi",
                    Data = customerDebtSummary
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CustomerCode} kodlu müşterinin borç özeti alınırken hata oluştu", customerCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{customerCode} kodlu müşterinin borç özeti alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Tüm müşterilerin borç özetlerini getirir
        /// </summary>
        [HttpGet("summary/all")]
        public async Task<ActionResult<List<CustomerDebtSummaryResponse>>> GetAllCustomerDebtSummaries([FromQuery] string langCode = "TR")
        {
            try
            {
                var customerDebtSummaries = await _customerDebtService.GetAllCustomerDebtSummariesAsync(langCode);
                return Ok(new ApiResponse<List<CustomerDebtSummaryResponse>>
                {
                    Success = true,
                    Message = "Tüm müşterilerin borç özetleri başarıyla getirildi",
                    Data = customerDebtSummaries
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm müşterilerin borç özetleri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Tüm müşterilerin borç özetleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
    }
}
