using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Product
{
    public class ProductVariantModel
    {
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string ColorCode { get; set; }
        public string ColorDescription { get; set; }
        public string ManufacturerColorCode { get; set; }
        public string ItemDim1Code { get; set; }
        public string ItemDim2Code { get; set; }
        public string ItemDim3Code { get; set; }
        public string BarcodeTypeCode { get; set; }
        public string Barcode { get; set; }
        public bool NotHaveBarcodes { get; set; }
        public decimal? Qty { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeDescription { get; set; }
        public string UnitOfMeasureCode1 { get; set; }
        public string UnitOfMeasureCode2 { get; set; }
        public string ProductHierarchyID { get; set; }
        public string ProductHierarchyLevel01 { get; set; }
        public string ProductHierarchyLevel02 { get; set; }
        public string ProductHierarchyLevel03 { get; set; }
        public string ProductHierarchyLevel04 { get; set; }
        public string ProductHierarchyLevel05 { get; set; }
        public string ProductHierarchyLevel06 { get; set; }
        public string ProductCollectionGrCode { get; set; }
        public string SeasonCode { get; set; }
        public string SeasonDescription { get; set; }
        public string CollectionCode { get; set; }
        public string CollectionDescription { get; set; }
        public string StoryBoardCode { get; set; }
        public string StoryBoardDescription { get; set; }
        public bool IsBlocked { get; set; }
        
        // Fiyat bilgileri
        public decimal SalesPrice1 { get; set; }
        public decimal? VatRate { get; set; }
    }
}
