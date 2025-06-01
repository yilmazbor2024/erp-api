// ExchangeRateController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ErpMobile.Api.Models.Responses;
using ErpMobile.Api.Interfaces;
using ErpMobile.Api.Models.Dto;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/exchange-rates")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
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
            startDate ??= DateTime.Today.AddMonths(-1);
            endDate ??= DateTime.Today;

            var result = await _exchangeRateService.GetExchangeRatesAsync(
                startDate.Value, 
                endDate.Value,
                source,
                page, 
                pageSize);

            return Ok(result);
        }

        // GET: api/exchange-rates/latest?date=2025-06-01
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<ExchangeRateDto>>> GetLatestExchangeRates(
            [FromQuery] DateTime? date = null,
            [FromQuery] string source = null)
        {
            date ??= DateTime.Today;
            var rates = await _exchangeRateService.GetExchangeRatesByDateAsync(date.Value, source);
            return Ok(rates);
        }
        
        // GET: api/exchange-rates/by-date?date=2025-06-01
        [HttpGet("by-date")]
        public async Task<ActionResult<IEnumerable<ExchangeRateDto>>> GetExchangeRatesByDate(
            [FromQuery] DateTime? date = null,
            [FromQuery] string source = null)
        {
            date ??= DateTime.Today;
            var rates = await _exchangeRateService.GetExchangeRatesByDateAsync(date.Value, source);
            return Ok(rates);
        }
        
        // GET: api/exchange-rates/conversion?fromCurrency=USD&toCurrency=TRY&date=2025-06-01
        [HttpGet("conversion")]
        public async Task<ActionResult<object>> GetConversionRate(
            [FromQuery] string fromCurrency,
            [FromQuery] string toCurrency,
            [FromQuery] DateTime? date = null,
            [FromQuery] string source = null)
        {
            if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
            {
                return BadRequest("Both fromCurrency and toCurrency are required");
            }
            
            date ??= DateTime.Today;
            var rate = await _exchangeRateService.GetConversionRateAsync(fromCurrency, toCurrency, date.Value, source);
            
            return Ok(new { rate });
        }
        

    }
}