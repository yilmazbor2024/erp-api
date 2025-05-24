using System;

namespace ErpMobile.Api.Models.Inventory
{
    public class InventoryStockModel
    {
        public string ItemTypeCode { get; set; }
        public string UsedBarcode { get; set; }
        public string ItemDescription { get; set; }
        public string ColorDescription { get; set; }
        public string BinCode { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public string BarcodeTypeCode { get; set; }
        public string ColorCode { get; set; }
        public string ItemDim1Code { get; set; } // Beden
        public string ItemDim2TypeCode { get; set; }
        public string ItemDim2Code { get; set; }
        public string ItemDim3Code { get; set; }
        public decimal Qty { get; set; } // Stok miktarı
        public bool VariantIsBlocked { get; set; }
        public bool IsNotExist { get; set; }
    }
}
