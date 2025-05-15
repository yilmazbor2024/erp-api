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
    public class CustomerCreditController : ControllerBase
    {
        private readonly ICustomerCreditService _customerCreditService;
        private readonly ILogger<CustomerCreditController> _logger;

        public CustomerCreditController(ICustomerCreditService customerCreditService, ILogger<CustomerCreditController> logger)
        {
            _customerCreditService = customerCreditService;
            _logger = logger;
        }

        /// <summary>
        /// Tüm müşteri alacaklarını getirir
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<CustomerCreditResponse>>> GetAllCustomerCredits([FromQuery] string langCode = "TR")
        {
            try
            {
                var customerCredits = await _customerCreditService.GetAllCustomerCreditsAsync(langCode);
                return Ok(new ApiResponse<List<CustomerCreditResponse>>
                {
                    Success = true,
                    Message = "Müşteri alacakları başarıyla getirildi",
                    Data = customerCredits
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri alacakları alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Müşteri alacakları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir müşterinin alacaklarını getirir
        /// </summary>
        [HttpGet("customer/{customerCode}")]
        public async Task<ActionResult<List<CustomerCreditResponse>>> GetCustomerCreditsByCustomerCode(string customerCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var customerCredits = await _customerCreditService.GetCustomerCreditsByCustomerCodeAsync(customerCode, langCode);
                return Ok(new ApiResponse<List<CustomerCreditResponse>>
                {
                    Success = true,
                    Message = $"{customerCode} kodlu müşterinin alacakları başarıyla getirildi",
                    Data = customerCredits
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CustomerCode} kodlu müşterinin alacakları alınırken hata oluştu", customerCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{customerCode} kodlu müşterinin alacakları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir para birimine ait müşteri alacaklarını getirir
        /// </summary>
        [HttpGet("currency/{currencyCode}")]
        public async Task<ActionResult<List<CustomerCreditResponse>>> GetCustomerCreditsByCurrency(string currencyCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var customerCredits = await _customerCreditService.GetCustomerCreditsByCurrencyAsync(currencyCode, langCode);
                return Ok(new ApiResponse<List<CustomerCreditResponse>>
                {
                    Success = true,
                    Message = $"{currencyCode} para birimine ait müşteri alacakları başarıyla getirildi",
                    Data = customerCredits
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CurrencyCode} para birimine ait müşteri alacakları alınırken hata oluştu", currencyCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{currencyCode} para birimine ait müşteri alacakları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir ödeme tipine ait müşteri alacaklarını getirir
        /// </summary>
        [HttpGet("payment-type/{paymentTypeCode}")]
        public async Task<ActionResult<List<CustomerCreditResponse>>> GetCustomerCreditsByPaymentType(int paymentTypeCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var customerCredits = await _customerCreditService.GetCustomerCreditsByPaymentTypeAsync(paymentTypeCode, langCode);
                return Ok(new ApiResponse<List<CustomerCreditResponse>>
                {
                    Success = true,
                    Message = $"{paymentTypeCode} ödeme tipine ait müşteri alacakları başarıyla getirildi",
                    Data = customerCredits
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{PaymentTypeCode} ödeme tipine ait müşteri alacakları alınırken hata oluştu", paymentTypeCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{paymentTypeCode} ödeme tipine ait müşteri alacakları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Vadesi geçmiş müşteri alacaklarını getirir
        /// </summary>
        [HttpGet("overdue")]
        public async Task<ActionResult<List<CustomerCreditResponse>>> GetOverdueCustomerCredits([FromQuery] string langCode = "TR")
        {
            try
            {
                var customerCredits = await _customerCreditService.GetOverdueCustomerCreditsAsync(langCode);
                return Ok(new ApiResponse<List<CustomerCreditResponse>>
                {
                    Success = true,
                    Message = "Vadesi geçmiş müşteri alacakları başarıyla getirildi",
                    Data = customerCredits
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Vadesi geçmiş müşteri alacakları alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Vadesi geçmiş müşteri alacakları alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir müşterinin alacak özetini getirir
        /// </summary>
        [HttpGet("summary/customer/{customerCode}")]
        public async Task<ActionResult<List<CustomerCreditSummaryResponse>>> GetCustomerCreditSummary(string customerCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var customerCreditSummary = await _customerCreditService.GetCustomerCreditSummaryAsync(customerCode, langCode);
                return Ok(new ApiResponse<List<CustomerCreditSummaryResponse>>
                {
                    Success = true,
                    Message = $"{customerCode} kodlu müşterinin alacak özeti başarıyla getirildi",
                    Data = customerCreditSummary
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CustomerCode} kodlu müşterinin alacak özeti alınırken hata oluştu", customerCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{customerCode} kodlu müşterinin alacak özeti alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Tüm müşterilerin alacak özetlerini getirir
        /// </summary>
        [HttpGet("summary/all")]
        public async Task<ActionResult<List<CustomerCreditSummaryResponse>>> GetAllCustomerCreditSummaries([FromQuery] string langCode = "TR")
        {
            try
            {
                var customerCreditSummaries = await _customerCreditService.GetAllCustomerCreditSummariesAsync(langCode);
                return Ok(new ApiResponse<List<CustomerCreditSummaryResponse>>
                {
                    Success = true,
                    Message = "Tüm müşterilerin alacak özetleri başarıyla getirildi",
                    Data = customerCreditSummaries
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm müşterilerin alacak özetleri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Tüm müşterilerin alacak özetleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
    }
}
