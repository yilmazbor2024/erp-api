using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Payment;
using ErpMobile.Api.Models;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services.CashAccount;
using ErpApi.Models.Payments;
using ErpApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    [Route("api/v1")]
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
        
        /// <summary>
        /// Kasa hareketleri raporunu getirir
        /// </summary>
        [HttpGet("cash-transactions")]
        public async Task<ActionResult<ApiResponse<List<CashTransactionReportItem>>>> GetCashTransactions(
            [FromQuery] string startDate = null,
            [FromQuery] string endDate = null,
            [FromQuery] string cashAccountCode = null,
            [FromQuery] string transactionType = null,
            [FromQuery] string currencyCode = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortField = "DocumentDate",
            [FromQuery] string sortOrder = "DESC")
        {
            try
            {
                _logger.LogInformation("Kasa hareketleri raporu endpoint'i çağrıldı");
                
                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                }
                
                // Varsayılan tarih değerleri
                if (string.IsNullOrEmpty(startDate))
                {
                    startDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                }
                
                if (string.IsNullOrEmpty(endDate))
                {
                    endDate = DateTime.Now.ToString("yyyyMMdd");
                }
                
                var request = new CashTransactionReportRequest
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    CashAccountCode = cashAccountCode,
                    TransactionType = transactionType,
                    CurrencyCode = currencyCode,
                    Page = page,
                    PageSize = pageSize,
                    SortField = sortField,
                    SortOrder = sortOrder
                };
                
                var result = await _paymentService.GetCashTransactionReportAsync(request);
                
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketleri raporu alınırken hata oluştu: {Message}", ex.Message);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa hareketleri raporu alınırken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Kasa hareketi detayını getirir
        /// </summary>
        [HttpGet("cash-transaction/{transactionNumber}")]
        public async Task<ActionResult<ApiResponse<CashTransactionReportItem>>> GetCashTransactionDetail(string transactionNumber)
        {
            try
            {
                _logger.LogInformation("Kasa hareketi detay endpoint'i çağrıldı. İşlem No: {TransactionNumber}", transactionNumber);
                
                if (string.IsNullOrEmpty(transactionNumber))
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "İşlem numarası gereklidir"
                    });
                }
                
                // Tek bir işlem için rapor isteği oluştur
                var request = new CashTransactionReportRequest
                {
                    StartDate = "20200101", // Geniş bir tarih aralığı kullan
                    EndDate = DateTime.Now.ToString("yyyyMMdd")
                };
                
                var result = await _paymentService.GetCashTransactionReportAsync(request);
                
                if (!result.Success)
                {
                    return BadRequest(result);
                }
                
                // İşlem numarasına göre filtrele
                var transaction = result.Data.FirstOrDefault(t => t.TransactionNumber == transactionNumber);
                
                if (transaction == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = $"{transactionNumber} numaralı kasa hareketi bulunamadı"
                    });
                }
                
                return Ok(new ApiResponse<CashTransactionReportItem>
                {
                    Success = true,
                    Message = "Kasa hareketi detayı başarıyla getirildi",
                    Data = transaction
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketi detayı alınırken hata oluştu: {Message}", ex.Message);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa hareketi detayı alınırken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Kasa tahsilat fişlerini listeler
        /// </summary>
        [HttpGet("payment/cash-voucher/receipts")]
        public async Task<ActionResult<ApiResponse<object>>> GetCashReceiptVouchers(
            [FromQuery] string startDate = null,
            [FromQuery] string endDate = null,
            [FromQuery] string cashAccountCode = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Kasa tahsilat fişleri endpoint'i çağrıldı
                
                // Varsayılan tarih değerleri
                if (string.IsNullOrEmpty(startDate))
                {
                    startDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                }
                
                if (string.IsNullOrEmpty(endDate))
                {
                    endDate = DateTime.Now.ToString("yyyyMMdd");
                }

                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                }
                
                // İstek parametreleri işleniyor
                
                var result = await _paymentService.GetCashReceiptVouchersAsync(startDate, endDate, cashAccountCode, page, pageSize, userName);
                
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { items = result.Data, total = result.TotalCount },
                    Message = "Kasa tahsilat fişleri başarıyla getirildi"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa tahsilat fişleri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa tahsilat fişleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Kasa virman fişlerini listeler
        /// </summary>
        [HttpGet("payment/cash-voucher/transfers")]
        public async Task<ActionResult<ApiResponse<object>>> GetCashTransferVouchers(
            [FromQuery] string startDate = null,
            [FromQuery] string endDate = null,
            [FromQuery] string cashAccountCode = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Kasa virman fişleri endpoint'i çağrıldı
                
                // Varsayılan tarih değerleri
                if (string.IsNullOrEmpty(startDate))
                {
                    startDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                }
                
                if (string.IsNullOrEmpty(endDate))
                {
                    endDate = DateTime.Now.ToString("yyyyMMdd");
                }

                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                }
                
                // İstek parametreleri işleniyor
                
                var result = await _paymentService.GetCashTransferVouchersAsync(startDate, endDate, cashAccountCode, page, pageSize, userName);
                
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { items = result.Data, total = result.TotalCount },
                    Message = "Kasa virman fişleri başarıyla getirildi"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa virman fişleri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa virman fişleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Kasa özet bilgilerini getirir
        /// </summary>
        [HttpGet("cash-summary")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CashSummaryResponse>>), 200)]
        public async Task<IActionResult> GetCashSummary(
            [FromQuery] string cashAccountCode = null)
        {
            _logger.LogInformation("API Request received: GET /api/v1/payment/cash-summary");
            _logger.LogInformation($"Query Parameters: cashAccountCode={cashAccountCode}");
            
            try
            {
                var userName = User.Identity.Name;
                _logger.LogInformation($"Calling PaymentService.GetCashSummaryAsync with parameters: cashAccountCode={cashAccountCode}, userName={userName}");
                var result = await _paymentService.GetCashSummaryAsync(cashAccountCode, userName);
                
                if (!result.Success)
                {
                    _logger.LogWarning($"GetCashSummaryAsync returned unsuccessful result: {result.Message}");
                    return BadRequest(result);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa özet bilgileri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa özet bilgileri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Kasa hareketini günceller
        /// </summary>
        [HttpPut("cash-transaction")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> UpdateCashTransaction([FromBody] CashUpdateRequest request)
        {
            _logger.LogInformation("API Request received: PUT /api/v1/payment/cash-transaction");
            _logger.LogInformation($"Request Body: CashHeaderId={request.CashHeaderId}, CashTransNumber={request.CashTransNumber}");
            
            try
            {
                var userName = User.Identity.Name;
                _logger.LogInformation($"Calling PaymentService.UpdateCashTransactionAsync with parameters: request={request}, userName={userName}");
                var result = await _paymentService.UpdateCashTransactionAsync(request, userName);
                
                if (!result.Success)
                {
                    _logger.LogWarning($"UpdateCashTransactionAsync returned unsuccessful result: {result.Message}");
                    return BadRequest(result);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketi güncellenirken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa hareketi güncellenirken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Kasa hareketini siler
        /// </summary>
        [HttpDelete("cash-transaction/{cashHeaderId}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteCashTransaction(
            [FromRoute] string cashHeaderId,
            [FromQuery] string cashNumber)
        {
            _logger.LogInformation("API Request received: DELETE /api/v1/payment/cash-transaction/{cashHeaderId}");
            _logger.LogInformation($"Route Parameters: cashHeaderId={cashHeaderId}, Query Parameters: cashNumber={cashNumber}");
            
            try
            {
                var userName = User.Identity.Name;
                _logger.LogInformation($"Calling PaymentService.DeleteCashTransactionAsync with parameters: cashHeaderId={cashHeaderId}, cashNumber={cashNumber}, userName={userName}");
                var result = await _paymentService.DeleteCashTransactionAsync(cashHeaderId, cashNumber, userName);
                
                if (!result.Success)
                {
                    _logger.LogWarning($"DeleteCashTransactionAsync returned unsuccessful result: {result.Message}");
                    return BadRequest(result);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketi silinirken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa hareketi silinirken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Kasa hareketlerini getirir
        /// </summary>
        [HttpGet("cash-balance")]
        [ProducesResponseType(typeof(PagedApiResponse<CashBalanceResponse>), 200)]
        public async Task<IActionResult> GetCashBalances(
            [FromQuery] string startDate = null,
            [FromQuery] string endDate = null,
            [FromQuery] string cashAccountCode = null,
            [FromQuery] string searchText = null,
            [FromQuery] string cashTransTypeCode = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            _logger.LogInformation("API Request received: GET /api/v1/payment/cash-balance");
            _logger.LogInformation($"Query Parameters: startDate={startDate}, endDate={endDate}, cashAccountCode={cashAccountCode}, searchText={searchText}, cashTransTypeCode={cashTransTypeCode}, page={page}, pageSize={pageSize}");
            
            try
            {
                // Varsayılan tarih değerleri
                if (string.IsNullOrEmpty(startDate))
                {
                    startDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                    _logger.LogInformation($"startDate was empty, using default value: {startDate}");
                }
                else
                {
                    _logger.LogInformation($"Using provided startDate: {startDate}");
                }
                
                if (string.IsNullOrEmpty(endDate))
                {
                    endDate = DateTime.Now.ToString("yyyyMMdd");
                    _logger.LogInformation($"endDate was empty, using default value: {endDate}");
                }
                else
                {
                    _logger.LogInformation($"Using provided endDate: {endDate}");
                }

                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                    _logger.LogInformation("userName was empty, using default value: System");
                }
                else
                {
                    _logger.LogInformation($"Request made by user: {userName}");
                }
                
                _logger.LogInformation($"Calling PaymentService.GetCashBalancesAsync with parameters: startDate={startDate}, endDate={endDate}, cashAccountCode={cashAccountCode}, userName={userName}, page={page}, pageSize={pageSize}, searchText={searchText}, cashTransTypeCode={cashTransTypeCode}");
                var result = await _paymentService.GetCashBalancesAsync(startDate, endDate, cashAccountCode, userName, page, pageSize, searchText, cashTransTypeCode);
                
                if (!result.Success)
                {
                    _logger.LogWarning($"GetCashBalancesAsync returned unsuccessful result: {result.Message}");
                    return StatusCode(500, result);
                }
                
                _logger.LogInformation($"Successfully retrieved cash balances. Count: {result.Data?.Count ?? 0}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa bakiyeleri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<List<CashBalanceResponse>>
                {
                    Success = false,
                    Message = "Kasa bakiyeleri alınırken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Kasa hesaplarını listeler
        /// </summary>
        /// <returns>Kasa hesapları listesi</returns>
        [HttpGet("payment/cash-accounts")]
        public async Task<ActionResult<IEnumerable<CashAccountResponse>>> GetCashAccounts()
        {
            _logger.LogInformation("API Request received: GET /api/v1/payment/cash-accounts");
            try
            {
                // Get the CashAccountService from the service provider
                var cashAccountService = HttpContext.RequestServices.GetService(typeof(ICashAccountService)) as ICashAccountService;
                
                if (cashAccountService == null)
                {
                    _logger.LogError("ICashAccountService could not be resolved");
                    return StatusCode(500, "Internal server error: Service not available");
                }
                
                _logger.LogInformation("Calling CashAccountService.GetCashAccountsAsync()");
                var cashAccounts = await cashAccountService.GetCashAccountsAsync();
                _logger.LogInformation($"Retrieved {cashAccounts?.Count() ?? 0} cash accounts successfully");
                
                return Ok(new ApiResponse<IEnumerable<CashAccountResponse>>
                {
                    Success = true,
                    Message = "Kasa hesapları başarıyla getirildi",
                    Data = cashAccounts
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cash accounts");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa hesapları alınırken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
        
        /// <summary>
        /// Kasa tediye fişlerini listeler
        /// </summary>
        [HttpGet("payment/cash-voucher/payments")]
        public async Task<ActionResult<ApiResponse<object>>> GetCashPaymentVouchers(
            [FromQuery] string startDate = null,
            [FromQuery] string endDate = null,
            [FromQuery] string cashAccountCode = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Kasa tediye fişleri endpoint'i çağrıldı
                
                // Varsayılan tarih değerleri
                if (string.IsNullOrEmpty(startDate))
                {
                    startDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                }
                
                if (string.IsNullOrEmpty(endDate))
                {
                    endDate = DateTime.Now.ToString("yyyyMMdd");
                }

                // Kullanıcı adını al
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "System";
                }
                
                // İstek parametreleri işleniyor
                
                var result = await _paymentService.GetCashPaymentVouchersAsync(startDate, endDate, cashAccountCode, page, pageSize, userName);
                
                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = new { items = result.Data, total = result.TotalCount },
                    Message = "Kasa tediye fişleri başarıyla getirildi"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa tediye fişleri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa tediye fişleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
    }
}
