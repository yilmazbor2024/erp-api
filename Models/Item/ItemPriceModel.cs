using System;

namespace ErpMobile.Api.Models.Item
{
    public class ItemPriceModel
    {
        public string ItemCode { get; set; }
        public string PriceListCode { get; set; }
        public string PriceListDescription { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsVatIncluded { get; set; }
        public bool IsBlocked { get; set; }
    }
}
