using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Invoice
{
    public class CreateInvoiceRequest
    {
        // Fatura başlık bilgileri
        [Required]
        public string InvoiceNumber { get; set; }
        
        public bool IsReturn { get; set; }
        
        public bool IsEInvoice { get; set; }
        
        
        
        [Required]
        public DateTime InvoiceDate { get; set; }
        
        public string InvoiceTime { get; set; }
        
        [Required]
        public int CurrAccTypeCode { get; set; }
        
        public string VendorCode { get; set; }
        
        public string CustomerCode { get; set; }
        
        public string RetailCustomerCode { get; set; }
        
        public string StoreCurrAccCode { get; set; }
        
        public string EmployeeCode { get; set; }
        
        public string SubCurrAccCode { get; set; }
        
        public int? SubCurrAccID { get; set; }
        
        public bool IsCreditSale { get; set; }
        
        public string ProcessCode { get; set; }
        
        public string TransTypeCode { get; set; }
        
        [Required]
        public string DocCurrencyCode { get; set; }
        
        public decimal? ExchangeRate { get; set; }
        
        public string Series { get; set; }
        
        public string SeriesNumber { get; set; }
        
        public string EInvoiceNumber { get; set; }
        
        [Required]
        public string CompanyCode { get; set; }
        
        public string OfficeCode { get; set; }
        
        public string StoreCode { get; set; }
        
        [Required]
        public string WarehouseCode { get; set; }
        
        public string ImportFileNumber { get; set; }
        
        public string ExportFileNumber { get; set; }
        
        public string ExportTypeCode { get; set; }
        
        public string PosTerminalID { get; set; }
        
        public string TaxTypeCode { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public bool IsSuspended { get; set; }
        
        public string ApplicationCode { get; set; }
        
        public int? ApplicationID { get; set; }
        
 
        
        public string FormType { get; set; }
        
        public string DocumentTypeCode { get; set; }
        
        public string Notes { get; set; }
        
        // Fatura detay bilgileri
        [Required]
        public List<CreateInvoiceDetailRequest> Details { get; set; }
    }

    public class CreateInvoiceDetailRequest
    {
        public int LineNumber { get; set; }
        
        [Required]
        public string ItemCode { get; set; }
        
        public string ItemTypeCode { get; set; }
        
        [Required]
        public string UnitOfMeasureCode { get; set; }
        
        [Required]
        public decimal Quantity { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        public decimal DiscountRate { get; set; }
        
        public decimal VatRate { get; set; }
        
        public string WarehouseCode { get; set; }
        
        public string LocationCode { get; set; }
        
        public string SerialNumber { get; set; }
        
        public string BatchCode { get; set; }
        
        public string Notes { get; set; }
        
        // Eksik özellikler
        public string CurrencyCode { get; set; }
        
        public decimal? ExchangeRate { get; set; }
        
        public bool IsGift { get; set; }
        
        public bool IsPromotional { get; set; }
        
        public string SalesPersonCode { get; set; }
        
        public string ProductTypeCode { get; set; }
        
        public string PromotionCode { get; set; }
        
        // Veritabanı eşleşmesi için gerekli özellikler
        public decimal Qty => Quantity;
        
        public string UnitCode => UnitOfMeasureCode;
        
        public string ProductCode => ItemCode;
    }
}
