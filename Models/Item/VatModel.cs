using System;

namespace ErpMobile.Api.Models.Item
{
    public class VatModel
    {
        public string VatCode { get; set; }
        public string VatDescription { get; set; }
        public decimal VatRate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
