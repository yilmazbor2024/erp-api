using System;

namespace Erp.Models.ExchangeRate
{
    public class ExchangeRateDto
    {
        public DateTime Date { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal ForexBuying { get; set; }
        public decimal ForexSelling { get; set; }
        public decimal BanknoteBuying { get; set; }
        public decimal BanknoteSelling { get; set; }
        public int Unit { get; set; }
        public string Source { get; set; }
    }
}
