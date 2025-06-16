using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ErpMobile.Api.Data;
using ErpMobile.Api.Models.Common;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Services.Interfaces;

namespace ErpMobile.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<CurrencyController> _logger;
        private readonly ErpDbContext _context;
        private readonly ITokenValidationService _tokenValidationService;

        public CurrencyController(ICurrencyService currencyService, ILogger<CurrencyController> logger, ErpDbContext context, ITokenValidationService tokenValidationService)
        {
            _currencyService = currencyService;
            _logger = logger;
            _context = context;
            _tokenValidationService = tokenValidationService;
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
        /// Para birimi listesini token ile getirir
        /// </summary>
        /// <param name="token">Doğrulama token'ı</param>
        /// <param name="langCode">Dil kodu</param>
        /// <returns>Para birimi listesi</returns>
        [AllowAnonymous]
        [HttpGet("currencies-with-token")]
        [ProducesResponseType(typeof(ApiResponse<List<CurrencyResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CurrencyResponse>>> GetCurrenciesWithToken([FromQuery] string token, [FromQuery] string langCode = "TR")
        {
            try
            {
                // Token doğrulama
                if (!_tokenValidationService.ValidateToken(token, TokenScope.CustomerRegistration))
                {
                    return Unauthorized(new ApiResponse<string>(null, false, "Invalid or expired token or token not authorized for this operation."));
                }

                var currencies = await _currencyService.GetCurrenciesAsync(langCode);
                return Ok(new ApiResponse<List<CurrencyResponse>>(currencies, true, "Para birimleri başarıyla alındı."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Para birimleri token ile alınırken hata oluştu. Token: {Token}, Dil Kodu: {LangCode}", token, langCode);
                return StatusCode(500, new ApiResponse<string>(null, false, "Para birimleri alınırken bir hata oluştu", ex.Message));
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

        /// <summary>
        /// Günlük döviz kurlarını getirir
        /// </summary>
        /// <param name="date">Tarih (varsayılan: bugün)</param>
        /// <param name="baseCurrencyCode">Baz para birimi kodu (varsayılan: TRY)</param>
        /// <returns>Döviz kurları listesi</returns>
        [HttpGet("exchange-rates")]
        public async Task<ActionResult<ApiResponse<List<ExchangeRateResponse>>>> GetExchangeRates(
            [FromQuery] DateTime? date = null,
            [FromQuery] string baseCurrencyCode = "TRY")
        {
            try
            {
                var effectiveDate = date ?? DateTime.Today;
                
                var query = @"
                    SELECT 
                        CurrencyCode,
                        RelationCurrencyCode,
                        Date,
                        Rate,
                        EndOfDayRate,
                        ExchangeTypeCode
                    FROM dbo.AllExchangeRates
                    WHERE RelationCurrencyCode = @BaseCurrencyCode
                    AND Date = @EffectiveDate
                    ORDER BY CurrencyCode";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@BaseCurrencyCode", baseCurrencyCode),
                    new SqlParameter("@EffectiveDate", effectiveDate)
                };

                var exchangeRates = new List<ExchangeRateResponse>();
                
                using (var reader = await _context.ExecuteReaderAsync(query, parameters.ToArray()))
                {
                    while (await reader.ReadAsync())
                    {
                        var currencyCode = reader["CurrencyCode"].ToString();
                        
                        // Para birimi detaylarını almak için ayrı bir sorgu çalıştırıyoruz
                        var currencyQuery = "SELECT CurrencyDescription, Symbol FROM cdCurrencyDesc WHERE CurrencyCode = @CurrencyCode AND LangCode = 'TR'";
                        var currencyParams = new List<SqlParameter>
                        {
                            new SqlParameter("@CurrencyCode", currencyCode)
                        };
                        
                        string currencyDescription = "";
                        string symbol = "";
                        
                        using (var currencyReader = await _context.ExecuteReaderAsync(currencyQuery, currencyParams.ToArray()))
                        {
                            if (await currencyReader.ReadAsync())
                            {
                                currencyDescription = currencyReader["CurrencyDescription"].ToString();
                                symbol = currencyReader["Symbol"].ToString();
                            }
                        }
                        
                        exchangeRates.Add(new ExchangeRateResponse
                        {
                            CurrencyCode = currencyCode,
                            CurrencyDescription = currencyDescription,
                            BaseCurrencyCode = reader["RelationCurrencyCode"].ToString(),
                            EffectiveDate = Convert.ToDateTime(reader["Date"]),
                            BuyingRate = Convert.ToDecimal(reader["Rate"]),
                            SellingRate = Convert.ToDecimal(reader["EndOfDayRate"]),
                            AverageRate = (Convert.ToDecimal(reader["Rate"]) + Convert.ToDecimal(reader["EndOfDayRate"])) / 2,
                            Symbol = symbol
                        });
                    }
                }
                
                return Ok(new ApiResponse<List<ExchangeRateResponse>>
                {
                    Success = true,
                    Message = "Döviz kurları başarıyla getirildi",
                    Data = exchangeRates
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Döviz kurları getirilirken hata oluştu. Tarih: {Date}, Baz Para Birimi: {BaseCurrencyCode}", 
                    date, baseCurrencyCode);
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Döviz kurları getirilirken bir hata oluştu",
                    Error = ex.Message
                });
            }
        }
    }
} 