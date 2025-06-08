using System;

namespace ErpMobile.Api.Models.Responses
{
    public class CashSummaryResponse
    {
        public string CashAccountCode { get; set; }
        public string CashAccountDescription { get; set; }
        public string CurrencyCode { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal Balance { get; set; }
    }
}
