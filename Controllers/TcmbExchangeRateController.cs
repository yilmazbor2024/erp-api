using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Erp.Models.ExchangeRate;
using Erp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Erp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TcmbExchangeRateController : ControllerBase
    {
        private readonly ILogger<TcmbExchangeRateController> _logger;
        private readonly TcmbExchangeRateService _tcmbService;

        public TcmbExchangeRateController(ILogger<TcmbExchangeRateController> logger, TcmbExchangeRateService tcmbService)
        {
            _logger = logger;
            _tcmbService = tcmbService;
        }

        /// <summary>
        /// TCMB'den güncel kurları getirir. Eğer bugün için kur yoksa (tatil, hafta sonu vb.), 
        /// son çalışma gününün kurlarını getirir.
        /// </summary>
        [HttpGet("latest")]
        public async Task<ActionResult<ApiResponse<List<ExchangeRateDto>>>> GetLatestRates()
        {
            try
            {
                var rates = await _tcmbService.GetLatestExchangeRatesAsync();
                return Ok(new ApiResponse<List<ExchangeRateDto>>
                {
                    Success = true,
                    Data = rates,
                    Message = "Kur bilgileri başarıyla getirildi."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kur bilgileri alınırken hata oluştu.");
                return StatusCode(500, new ApiResponse<List<ExchangeRateDto>>
                {
                    Success = false,
                    Message = "Kur bilgileri alınırken bir hata oluştu: " + ex.Message
                });
            }
        }

        /// <summary>
        /// TCMB'den son çalışma gününün kurlarını getirir.
        /// </summary>
        [HttpGet("last-working-day")]
        public async Task<ActionResult<ApiResponse<List<ExchangeRateDto>>>> GetLastWorkingDayRates()
        {
            try
            {
                var rates = await _tcmbService.GetLastWorkingDayRatesAsync();
                return Ok(new ApiResponse<List<ExchangeRateDto>>
                {
                    Success = true,
                    Data = rates,
                    Message = "Son çalışma günü kur bilgileri başarıyla getirildi."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Son çalışma günü kur bilgileri alınırken hata oluştu.");
                return StatusCode(500, new ApiResponse<List<ExchangeRateDto>>
                {
                    Success = false,
                    Message = "Son çalışma günü kur bilgileri alınırken bir hata oluştu: " + ex.Message
                });
            }
        }
        
        /// <summary>
        /// Günlük döviz kurlarını TCMB'den çekip veritabanına kaydeder.
        /// Bu endpoint günde bir kez çağrılmalıdır, tercihen sabah saatlerinde.
        /// </summary>
        [HttpPost("sync-daily")]
        public async Task<ActionResult<ApiResponse<bool>>> SyncDailyExchangeRates()
        {
            try
            {
                var result = await _tcmbService.SyncExchangeRatesAsync();
                return Ok(new ApiResponse<bool>
                {
                    Success = result,
                    Data = result,
                    Message = result 
                        ? "Günlük döviz kurları başarıyla senkronize edildi."
                        : "Döviz kurları senkronize edilirken bir sorun oluştu."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Günlük döviz kurları senkronize edilirken hata oluştu.");
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Günlük döviz kurları senkronize edilirken bir hata oluştu: " + ex.Message
                });
            }
        }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
