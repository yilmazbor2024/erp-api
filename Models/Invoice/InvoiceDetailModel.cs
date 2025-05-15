using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Invoice
{
    public class InvoiceDetailModel
    {
        public int InvoiceDetailID { get; set; }
        public int InvoiceHeaderID { get; set; }
        public string InvoiceNumber { get; set; }
        public int LineNumber { get; set; }
        
        // Ürün bilgileri
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ItemTypeCode { get; set; }
        public string ProductTypeCode { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public string SalesPersonCode { get; set; }
        public string PromotionCode { get; set; }
        
        // Veritabanı eşleşmesi için gerekli özellikler
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal Qty { get; set; }
        public string UnitCode { get; set; }
        
        // Miktar ve fiyat bilgileri
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal LineNetAmount { get; set; }
        public decimal LineTotalAmount { get; set; }
        
        // Para birimi bilgileri
        public string CurrencyCode { get; set; }
        public decimal? ExchangeRate { get; set; }
        
        // Diğer özellikler
        public bool IsGift { get; set; }
        public bool IsPromotional { get; set; }
        
        // Depo ve lokasyon bilgileri
        public string WarehouseCode { get; set; }
        public string LocationCode { get; set; }
        
        // Diğer bilgiler
        public string SerialNumber { get; set; }
        public string BatchCode { get; set; }
        public string Notes { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCancelled { get; set; }
        
        // Sistem bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
