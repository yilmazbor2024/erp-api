using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Invoice
{
    public class InvoiceHeaderModel
    {
        public string InvoiceNumber { get; set; }
        public bool IsReturn { get; set; }
        public bool IsEInvoice { get; set; } 
        public string InvoiceTypeCode { get; set; }
        public string InvoiceTypeDescription { get; set; }
        
        public DateTime InvoiceDate { get; set; }
        public string InvoiceTime { get; set; }
        public int CurrAccTypeCode { get; set; }
        
        public string VendorCode { get; set; }
        public string VendorDescription { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerDescription { get; set; }
        public string RetailCustomerCode { get; set; }
        public string StoreCurrAccCode { get; set; }
        public string StoreDescription { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstLastName { get; set; }
        
        public string SubCurrAccCode { get; set; }
        public string SubCurrAccCompanyName { get; set; }
        public bool IsCreditSale { get; set; }
        public string ProcessCode { get; set; }
        public int? TransTypeCode { get; set; }
        public string DocCurrencyCode { get; set; }
        public string CurrencyCode { get; set; }
        public string Series { get; set; }
        public string SeriesNumber { get; set; }
        public string EInvoiceNumber { get; set; }
        public string CompanyCode { get; set; }
        public string OfficeCode { get; set; }
        public string StoreCode { get; set; }
        public string WarehouseCode { get; set; }
        public string ImportFileNumber { get; set; }
        public string ExportFileNumber { get; set; }
        public string ExportTypeCode { get; set; }
        public string PosTerminalID { get; set; }
        public string TaxTypeCode { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsLocked { get; set; }
        public bool IsOrderBase { get; set; }
        public bool IsShipmentBase { get; set; }
        public bool IsPostingJournal { get; set; }
        public string JournalNumber { get; set; }
        public bool IsPrinted { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationDescription { get; set; }
        public string ApplicationID { get; set; }
        public string InvoiceHeaderID { get; set; }
     
        public int? FormType { get; set; }
        public int? DocumentTypeCode { get; set; }
        public int? PaymentTerm { get; set; }
        public string ShippingPostalAddressID { get; set; }
        public string BillingPostalAddressID { get; set; }
        public int? TaxExemptionCode { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string CurrAccCode { get; set; }
        public string LocalCurrencyCode { get; set; }
        public DateTime? JournalDate { get; set; }
        public DateTime? OperationDate { get; set; }
        public string OperationTime { get; set; }
        public DateTime? AverageDueDate { get; set; }
        
        // Ek özellikler
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsPaid { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // Veritabanından gelen ek alanlar
        public string CreatedUserName { get; set; }
        public string LastUpdatedUserName { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        
        // Fatura detayları
        public List<InvoiceDetailModel> Details { get; set; } = new List<InvoiceDetailModel>();
    }
}
