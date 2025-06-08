using System;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        /// <summary>
        /// Fatura için peşin ödeme işlemi yapar
        /// </summary>
        [HttpPost("cash-payment")]
        public async Task<ActionResult<ApiResponse<CashPaymentResponse>>> CreateCashPayment([FromBody] CashPaymentRequest request)
        {
            try
            {
                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                }

                var result = await _paymentService.CreateCashPaymentForInvoiceAsync(request, userName);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<CashPaymentResponse>
                    {
                        Success = true,
                        Message = result.Message,
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<CashPaymentResponse>
                    {
                        Success = false,
                        Message = result.Message,
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Peşin ödeme oluşturulurken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Peşin ödeme oluşturulurken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
    }
}
