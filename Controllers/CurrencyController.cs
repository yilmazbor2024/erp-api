using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Interfaces;
using erp_api.Models.Responses;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ICurrencyService currencyService, ILogger<CurrencyController> logger)
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        /// <summary>
        /// Para birimi listesini getirir
        /// </summary>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Para birimi listesi</returns>
        [HttpGet]
        public async Task<ActionResult<List<CurrencyResponse>>> GetCurrencies([FromQuery] string langCode = "TR")
        {
            try
            {
                var currencies = await _currencyService.GetCurrenciesAsync(langCode);
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Para birimleri alınırken hata oluştu. Dil Kodu: {LangCode}", langCode);
                return StatusCode(500, "Para birimleri alınırken bir hata oluştu");
            }
        }

        /// <summary>
        /// Para birimi detayını getirir
        /// </summary>
        /// <param name="code">Para birimi kodu</param>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Para birimi detayı</returns>
        [HttpGet("{code}")]
        public async Task<ActionResult<CurrencyResponse>> GetCurrencyByCode(string code, [FromQuery] string langCode = "TR")
        {
            try
            {
                var currency = await _currencyService.GetCurrencyByCodeAsync(code, langCode);
                
                if (currency == null)
                {
                    return NotFound($"Para birimi bulunamadı. Kod: {code}");
                }
                
                return Ok(currency);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Para birimi detayı alınırken hata oluştu. Para Birimi Kodu: {CurrencyCode}, Dil Kodu: {LangCode}", 
                    code, langCode);
                return StatusCode(500, "Para birimi detayı alınırken bir hata oluştu");
            }
        }
    }
} 