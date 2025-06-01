using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Dto;
using ErpMobile.Api.Models.Responses;

namespace ErpMobile.Api.Interfaces
{
    public interface IExchangeRateService
    {
        Task<PagedResult<ExchangeRateDto>> GetExchangeRatesAsync(
            DateTime startDate, 
            DateTime endDate,
            string source,
            int page,
            int pageSize);
            
        Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesByDateAsync(
            DateTime date,
            string source = null);
            
        Task<decimal> GetConversionRateAsync(
            string fromCurrency, 
            string toCurrency, 
            DateTime date,
            string source = null);
            
        Task<IEnumerable<object>> GetCrossRatesAsync(
            string baseCurrency,
            DateTime date,
            string source = null);
            
        Task<IEnumerable<ExchangeRateDto>> GetHistoricalRatesAsync(
            string currency,
            string relationCurrency,
            DateTime startDate,
            DateTime endDate,
            string source = null);
    }
}
