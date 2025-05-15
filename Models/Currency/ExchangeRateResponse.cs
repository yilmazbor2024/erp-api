using System;

namespace ErpMobile.Api.Models.Responses
{
    public class ExchangeRateResponse
    {
        public string CurrencyCode { get; set; }
        public string CurrencyDescription { get; set; }
        public string BaseCurrencyCode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal BuyingRate { get; set; }
        public decimal SellingRate { get; set; }
        public decimal AverageRate { get; set; }
        public string Symbol { get; set; }
    }
}
