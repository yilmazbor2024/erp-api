namespace ErpMobile.Api.Models.Responses
{
    public class CountryResponse
    {
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public bool UseVat { get; set; }
        public bool IsVatRequired { get; set; }
        public string TaxDecCode { get; set; }
        public string CountryISOCode { get; set; }
        public bool UseItemDim1Equ { get; set; }
        public bool IsItemDim1Required { get; set; }
        public bool UseItemDim2Equ { get; set; }
        public bool IsItemDim2Required { get; set; }
        public bool UseItemDim3Equ { get; set; }
        public bool IsItemDim3Required { get; set; }
        public bool ApplyPCTOnSelectedPaymentTypes { get; set; }
        public bool IsBlocked { get; set; }
        public string LangCode { get; set; }
        public string CountryDescription { get; set; }
    }
}
