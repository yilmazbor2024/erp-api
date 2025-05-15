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
    public class CashController : ControllerBase
    {
        private readonly ICashService _cashService;
        private readonly ILogger<CashController> _logger;

        public CashController(ICashService cashService, ILogger<CashController> logger)
        {
            _cashService = cashService;
            _logger = logger;
        }

        /// <summary>
        /// Tüm kasa hareketlerini getirir
        /// </summary>
        [HttpGet("transactions")]
        public async Task<ActionResult<List<CashTransactionResponse>>> GetAllCashTransactions([FromQuery] string langCode = "TR")
        {
            try
            {
                var cashTransactions = await _cashService.GetAllCashTransactionsAsync(langCode);
                return Ok(new ApiResponse<List<CashTransactionResponse>>
                {
                    Success = true,
                    Message = "Kasa hareketleri başarıyla getirildi",
                    Data = cashTransactions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kasa hareketleri alınırken hata oluştu");
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Kasa hareketleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir para birimine ait kasa hareketlerini getirir
        /// </summary>
        [HttpGet("transactions/currency/{currencyCode}")]
        public async Task<ActionResult<List<CashTransactionResponse>>> GetCashTransactionsByCurrency(string currencyCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var cashTransactions = await _cashService.GetCashTransactionsByCurrencyAsync(currencyCode, langCode);
                return Ok(new ApiResponse<List<CashTransactionResponse>>
                {
                    Success = true,
                    Message = $"{currencyCode} para birimine ait kasa hareketleri başarıyla getirildi",
                    Data = cashTransactions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CurrencyCode} para birimine ait kasa hareketleri alınırken hata oluştu", currencyCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{currencyCode} para birimine ait kasa hareketleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir hareket tipine ait kasa hareketlerini getirir
        /// </summary>
        [HttpGet("transactions/type/{cashTransTypeCode}")]
        public async Task<ActionResult<List<CashTransactionResponse>>> GetCashTransactionsByType(int cashTransTypeCode, [FromQuery] string langCode = "TR")
        {
            try
            {
                var cashTransactions = await _cashService.GetCashTransactionsByTypeAsync(cashTransTypeCode, langCode);
                return Ok(new ApiResponse<List<CashTransactionResponse>>
                {
                    Success = true,
                    Message = $"{cashTransTypeCode} tipine ait kasa hareketleri başarıyla getirildi",
                    Data = cashTransactions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CashTransTypeCode} tipine ait kasa hareketleri alınırken hata oluştu", cashTransTypeCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{cashTransTypeCode} tipine ait kasa hareketleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir para birimi ve hareket tipine ait kasa hareketlerini getirir
        /// </summary>
        [HttpGet("transactions/currency/{currencyCode}/type/{cashTransTypeCode}")]
        public async Task<ActionResult<List<CashTransactionResponse>>> GetCashTransactionsByCurrencyAndType(
            string currencyCode, 
            int cashTransTypeCode, 
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var cashTransactions = await _cashService.GetCashTransactionsByCurrencyAndTypeAsync(currencyCode, cashTransTypeCode, langCode);
                return Ok(new ApiResponse<List<CashTransactionResponse>>
                {
                    Success = true,
                    Message = $"{currencyCode} para birimi ve {cashTransTypeCode} tipine ait kasa hareketleri başarıyla getirildi",
                    Data = cashTransactions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{CurrencyCode} para birimi ve {CashTransTypeCode} tipine ait kasa hareketleri alınırken hata oluştu", 
                    currencyCode, cashTransTypeCode);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{currencyCode} para birimi ve {cashTransTypeCode} tipine ait kasa hareketleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli bir tarih aralığındaki kasa hareketlerini getirir
        /// </summary>
        [HttpGet("transactions/date-range")]
        public async Task<ActionResult<List<CashTransactionResponse>>> GetCashTransactionsByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, 
            [FromQuery] string langCode = "TR")
        {
            try
            {
                var cashTransactions = await _cashService.GetCashTransactionsByDateRangeAsync(startDate, endDate, langCode);
                return Ok(new ApiResponse<List<CashTransactionResponse>>
                {
                    Success = true,
                    Message = $"{startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy} tarih aralığındaki kasa hareketleri başarıyla getirildi",
                    Data = cashTransactions
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{StartDate} - {EndDate} tarih aralığındaki kasa hareketleri alınırken hata oluştu", 
                    startDate, endDate);
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = $"{startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy} tarih aralığındaki kasa hareketleri alınırken bir hata oluştu",
                    Data = ex.Message
                });
            }
        }
    }
}
