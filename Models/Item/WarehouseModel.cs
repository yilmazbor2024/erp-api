using System;

namespace ErpMobile.Api.Models.Item
{
    public class WarehouseModel
    {
        public string WarehouseCode { get; set; }
        public string WarehouseDescription { get; set; }
        public string WarehouseOwnerCode { get; set; }
        public string WarehouseTypeCode { get; set; }
        public string WarehouseCategoryCode { get; set; }
        public string OfficeCode { get; set; }
        public int CurrAccTypeCode { get; set; }
        public string CurrAccCode { get; set; }
        public bool PermitNegativeStock { get; set; }
        public bool WarnNegativeStock { get; set; }
        public bool IsDefault { get; set; }
        public bool IsBlocked { get; set; }
    }
}
