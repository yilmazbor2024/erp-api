using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Item
{
    public class ItemModel
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int ItemTypeCode { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ItemDimTypeCode { get; set; }
        public string ItemDimTypeDescription { get; set; }
        public string UnitCode { get; set; }
        public string UnitOfMeasureCode1 { get; set; }
        public string UnitOfMeasureCode2 { get; set; }
        public string VatCode { get; set; }
        public decimal VatRate { get; set; }
        public string CompanyBrandCode { get; set; }
        public bool UsePOS { get; set; }
        public bool UseStore { get; set; }
        public bool UseRoll { get; set; }
        public bool UseBatch { get; set; }
        public bool GenerateSerialNumber { get; set; }
        public bool UseSerialNumber { get; set; }
        public bool IsUTSDeclaratedItem { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
