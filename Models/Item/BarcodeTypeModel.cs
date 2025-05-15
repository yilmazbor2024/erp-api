using System;

namespace ErpMobile.Api.Models.Item
{
    public class BarcodeTypeModel
    {
        public string BarcodeTypeCode { get; set; }
        public string BarcodeTypeDescription { get; set; }
        public string StandardBarcodeTypeCode { get; set; }
        public string StandardBarcodeTypeDescription { get; set; }
        public bool IsBlocked { get; set; }
    }
}
