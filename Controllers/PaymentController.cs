using System;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Payment;
using ErpApi.Models.Payments;
using ErpApi.Services.Interfaces;
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
        private readonly ICreditPaymentService _creditPaymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ICreditPaymentService creditPaymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _creditPaymentService = creditPaymentService;
            _logger = logger;
        }

        /// <summary>
        /// Fatura için vadeli ödeme işlemi yapar
        /// </summary>
        [HttpPost("credit-payment")]
        public async Task<ActionResult<ApiResponse<CreditPaymentResponse>>> CreateCreditPayment([FromBody] CreditPaymentRequest request)
        {
            try
            {
                _logger.LogInformation("Vadeli ödeme işlemi başlatıldı. Fatura No: {InvoiceNumber}, Cari Kod: {CurrAccCode}", 
                    request.InvoiceNumber, request.CurrAccCode);

                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                }
                
                _logger.LogInformation("Vadeli ödeme işlemi için kullanıcı: {UserName}", userName);

                // Ödeme satırlarını logla
                if (request.PaymentRows != null && request.PaymentRows.Count > 0)
                {
                    foreach (var row in request.PaymentRows)
                    {
                        _logger.LogInformation("Vadeli ödeme satırı: Tutar={Amount}, DueDate={DueDate}, ParaBirimi={CurrencyCode}, Kur={ExchangeRate}",
                            row.Amount, row.DueDate.ToString("yyyy-MM-dd"), row.CurrencyCode, row.ExchangeRate);
                    }
                }

                _logger.LogInformation("CreditPaymentService.CreateCreditPaymentForInvoiceAsync çağrılıyor...");
                var result = await _creditPaymentService.CreateCreditPaymentForInvoiceAsync(request);
                _logger.LogInformation("CreditPaymentService.CreateCreditPaymentForInvoiceAsync tamamlandı. Sonuç: {Success}", result.Success);
                
                if (result.Success)
                {
                    _logger.LogInformation("Vadeli ödeme başarıyla oluşturuldu. DebitHeaderId: {DebitHeaderId}", result.DebitHeaderId);
                    return Ok(new ApiResponse<CreditPaymentResponse>
                    {
                        Success = true,
                        Message = result.Message,
                        Data = result
                    });
                }
                else
                {
                    _logger.LogWarning("Vadeli ödeme oluşturulamadı: {Message}", result.Message);
                    return BadRequest(new ApiResponse<CreditPaymentResponse>
                    {
                        Success = false,
                        Message = result.Message,
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Vadeli ödeme oluşturulurken hata oluştu: {Message}", ex.Message);
                _logger.LogError("Hata detayı: {StackTrace}", ex.StackTrace);
                
                if (ex.InnerException != null)
                {
                    _logger.LogError("İç hata: {InnerMessage}", ex.InnerException.Message);
                }
                
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Vadeli ödeme oluşturulurken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Fatura için peşin ödeme işlemi yapar
        /// </summary>
        [HttpPost("cash-payment")]
        public async Task<ActionResult<ApiResponse<CashPaymentResponse>>> CreateCashPayment([FromBody] CashPaymentRequest request)
        {
            try
            {
                _logger.LogInformation("Peşin ödeme işlemi başlatıldı. Fatura No: {InvoiceNumber}, Cari Kod: {CurrAccCode}", 
                    request.InvoiceNumber, request.CurrAccCode);

                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                }
                
                _logger.LogInformation("Peşin ödeme işlemi için kullanıcı: {UserName}", userName);

                _logger.LogInformation("PaymentService.CreateCashPaymentForInvoiceAsync çağrılıyor...");
                var result = await _paymentService.CreateCashPaymentForInvoiceAsync(request, userName);
                _logger.LogInformation("PaymentService.CreateCashPaymentForInvoiceAsync tamamlandı. Sonuç: {Success}", result.Success);
                
                if (result.Success)
                {
                    _logger.LogInformation("Peşin ödeme başarıyla oluşturuldu. CashHeaderId: {CashHeaderId}", result.CashHeaderId);
                    return Ok(new ApiResponse<CashPaymentResponse>
                    {
                        Success = true,
                        Message = result.Message,
                        Data = result
                    });
                }
                else
                {
                    _logger.LogWarning("Peşin ödeme oluşturulamadı: {Message}", result.Message);
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
                _logger.LogError(ex, "Peşin ödeme oluşturulurken hata oluştu: {Message}", ex.Message);
                _logger.LogError("Hata detayı: {StackTrace}", ex.StackTrace);
                
                if (ex.InnerException != null)
                {
                    _logger.LogError("İç hata: {InnerMessage}", ex.InnerException.Message);
                }
                
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
