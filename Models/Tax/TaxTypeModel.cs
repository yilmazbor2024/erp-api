using System;

namespace ErpMobile.Api.Models.Tax
{
    public class TaxTypeModel
    {
        public string TaxTypeCode { get; set; }
        public bool IsBlocked { get; set; }
        public string LangCode { get; set; }
        public string TaxTypeDescription { get; set; }
    }
}
