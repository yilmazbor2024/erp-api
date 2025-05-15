using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Item
{
    public class CreateItemRequest
    {
        [Required]
        public string ItemCode { get; set; }
        
        [Required]
        public string ItemDescription { get; set; }
        
        [Required]
        public string ProductTypeCode { get; set; }
        
        [Required]
        public string ItemDimTypeCode { get; set; }
        
        [Required]
        public string UnitOfMeasureCode1 { get; set; }
        
        public string UnitOfMeasureCode2 { get; set; }
        
        public string CompanyBrandCode { get; set; }
        
        public bool UsePOS { get; set; } = false;
        
        public bool UseStore { get; set; } = false;
        
        public bool UseRoll { get; set; } = false;
        
        public bool UseBatch { get; set; } = false;
        
        public bool GenerateSerialNumber { get; set; } = false;
        
        public bool UseSerialNumber { get; set; } = false;
        
        public bool IsUTSDeclaratedItem { get; set; } = false;
        
        public bool IsBlocked { get; set; } = false;
        
        public int ProductHierarchyID { get; set; } = 0;
    }
}
