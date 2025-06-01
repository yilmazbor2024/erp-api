using System;

namespace ErpMobile.Api.Models.Dto
{
    public class ExchangeRateDto
    {
        public DateTime Date { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyDescription { get; set; }
        public string RelationCurrencyCode { get; set; } = "TRY";
        public string RelationCurrencyDescription { get; set; } = "Türk Lirası";
        
        // Serbest Piyasa Kurları
        public decimal? FreeMarketBuyingRate { get; set; }
        public decimal? FreeMarketSellingRate { get; set; }
        
        // TCMB Kurları
        public decimal? CashBuyingRate { get; set; }
        public decimal? CashSellingRate { get; set; }
        public decimal? BanknoteBuyingRate { get; set; }
        public decimal? BanknoteSellingRate { get; set; }
        public decimal? BankForInformationPurposes { get; set; }
        
        // Kaynak bilgisi
        public string Source { get; set; }
    }
}
