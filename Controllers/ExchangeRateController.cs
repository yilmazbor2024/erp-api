// ExchangeRateController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Dto;
using Microsoft.Extensions.Logging;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/exchange-rates")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<ExchangeRateController> _logger;

        public ExchangeRateController(IExchangeRateService exchangeRateService, ILogger<ExchangeRateController> logger)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        // GET: api/exchange-rates?startDate=2025-01-01&endDate=2025-06-01&page=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResult<ExchangeRateDto>>> GetExchangeRates(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string source = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation($"GetExchangeRates called with parameters: startDate={startDate}, endDate={endDate}, source={source}, page={page}, pageSize={pageSize}");
            try
            {
                startDate ??= DateTime.Today.AddMonths(-1);
                endDate ??= DateTime.Today;

                var result = await _exchangeRateService.GetExchangeRatesAsync(
                    startDate.Value, 
                    endDate.Value,
                    source,
                    page, 
                    pageSize);

                _logger.LogInformation($"GetExchangeRates successful, returning {result.Items.Count()} rates");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetExchangeRates: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while fetching exchange rates");
            }
        }

        // GET: api/exchange-rates/latest?date=2025-06-01
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<ExchangeRateDto>>> GetLatestExchangeRates(
            [FromQuery] DateTime? date = null,
            [FromQuery] string source = null)
        {
            _logger.LogInformation($"GetLatestExchangeRates called with parameters: date={date}, source={source}");
            try
            {
                date ??= DateTime.Today;
                var rates = await _exchangeRateService.GetExchangeRatesByDateAsync(date.Value, source);

                _logger.LogInformation($"GetLatestExchangeRates successful, returning {rates.Count()} rates");
                return Ok(rates);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetLatestExchangeRates: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while fetching latest exchange rates");
            }
        }
        
        // GET: api/exchange-rates/by-date?date=2025-06-01
        [HttpGet("by-date")]
        public async Task<ActionResult<IEnumerable<ExchangeRateDto>>> GetExchangeRatesByDate(
            [FromQuery] DateTime? date = null,
            [FromQuery] string source = null)
        {
            _logger.LogInformation($"GetExchangeRatesByDate called with parameters: date={date}, source={source}");
            try
            {
                date ??= DateTime.Today;
                var rates = await _exchangeRateService.GetExchangeRatesByDateAsync(date.Value, source);

                _logger.LogInformation($"GetExchangeRatesByDate successful, returning {rates.Count()} rates");
                return Ok(rates);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetExchangeRatesByDate: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while fetching exchange rates by date");
            }
        }
        
        // GET: api/exchange-rates/conversion?fromCurrency=USD&toCurrency=TRY&date=2025-06-01
        [HttpGet("conversion")]
        public async Task<ActionResult<object>> GetConversionRate(
            [FromQuery] string fromCurrency,
            [FromQuery] string toCurrency,
            [FromQuery] DateTime? date = null,
            [FromQuery] string source = null)
        {
            _logger.LogInformation($"GetConversionRate called with parameters: fromCurrency={fromCurrency}, toCurrency={toCurrency}, date={date}, source={source}");
            try
            {
                if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
                {
                    _logger.LogWarning("GetConversionRate failed: fromCurrency and toCurrency are required");
                    return BadRequest("Both fromCurrency and toCurrency are required");
                }
                
                date ??= DateTime.Today;
                var rate = await _exchangeRateService.GetConversionRateAsync(fromCurrency, toCurrency, date.Value, source);
                
                _logger.LogInformation($"GetConversionRate successful, {fromCurrency} to {toCurrency} rate: {rate}");
                return Ok(new { rate });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetConversionRate: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while fetching conversion rate");
            }
        }
        
        // GET: api/exchange-rates/cross-rates?date=2025-06-01&baseCurrency=USD
        [HttpGet("cross-rates")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<object>>> GetCrossRates(
            [FromQuery] string baseCurrency = "USD",
            [FromQuery] DateTime? date = null,
            [FromQuery] string source = null)
        {
            _logger.LogInformation($"GetCrossRates called with parameters: baseCurrency={baseCurrency}, date={date}, source={source}");
            try
            {
                if (string.IsNullOrEmpty(baseCurrency))
                {
                    _logger.LogWarning("GetCrossRates failed: baseCurrency is required");
                    return BadRequest("baseCurrency is required");
                }
                
                date ??= DateTime.Today;
                _logger.LogInformation($"Calling GetCrossRatesAsync with parameters: baseCurrency={baseCurrency}, date={date}, source={source}");
                var crossRates = await _exchangeRateService.GetCrossRatesAsync(baseCurrency, date.Value, source);
                
                _logger.LogInformation($"GetCrossRates successful, returning {crossRates.Count()} cross rates for {baseCurrency}");
                return Ok(crossRates);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCrossRates: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while fetching cross rates");
            }
        }
        
        // GET: api/exchange-rates/historical?currency=USD&relationCurrency=TRY&startDate=2025-01-01&endDate=2025-06-01
        [HttpGet("historical")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ExchangeRateDto>>> GetHistoricalRates(
            [FromQuery] string currency,
            [FromQuery] string relationCurrency = "TRY",
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string source = null)
        {
            _logger.LogInformation($"GetHistoricalRates called with parameters: currency={currency}, relationCurrency={relationCurrency}, startDate={startDate}, endDate={endDate}, source={source}");
            try
            {
                if (string.IsNullOrEmpty(currency))
                {
                    _logger.LogWarning("GetHistoricalRates failed: currency is required");
                    return BadRequest("currency is required");
                }
                
                startDate ??= DateTime.Today.AddMonths(-1);
                endDate ??= DateTime.Today;
                
                _logger.LogInformation($"Calling GetHistoricalRatesAsync with parameters: currency={currency}, relationCurrency={relationCurrency}, startDate={startDate}, endDate={endDate}, source={source}");
                var rates = await _exchangeRateService.GetHistoricalRatesAsync(currency, relationCurrency, startDate.Value, endDate.Value, source);
                
                _logger.LogInformation($"GetHistoricalRates successful, returning {rates.Count()} rates");
                return Ok(rates);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetHistoricalRates: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while fetching historical rates");
            }
        }
    }
}