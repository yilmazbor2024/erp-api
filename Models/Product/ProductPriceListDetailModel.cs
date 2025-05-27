using System;

namespace ErpMobile.Api.Models.Product
{
    /// <summary>
    /// Ürün fiyat listesi detay modeli
    /// </summary>
    public class ProductPriceListDetailModel
    {
        // Sıralama ve tanımlayıcı bilgileri
        public int SortOrder { get; set; }
        public string HeaderID { get; set; }
        
        // Ürün bilgileri
        public string ItemTypeCode { get; set; }
        public string ItemTypeDescription { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        
        // Renk ve boyut bilgileri
        public string ColorCode { get; set; }
        public string ColorDescription { get; set; }
        public string ItemDim1Code { get; set; }
        public string ItemDim2Code { get; set; }
        public string ItemDim3Code { get; set; }
        
        // Birim ve ödeme bilgileri
        public string UnitOfMeasureCode { get; set; }
        public string PaymentPlanCode { get; set; }
        public string LineDescription { get; set; }
        
        // Fiyat bilgileri
        public decimal BirimFiyat { get; set; }
        public string DocCurrencyCode { get; set; }
        
        // Durum bilgileri
        public bool IsDisabled { get; set; }
        public DateTime? DisableDate { get; set; }
    }
}
