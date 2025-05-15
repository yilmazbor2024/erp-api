using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Invoice
{
    public class WholesaleInvoiceModel
    {
        public int InvoiceHeaderID { get; set; }
        
        public string InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public string CustomerCode { get; set; }
        
        public string CustomerName { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal TotalTax { get; set; }
        
        public decimal TotalDiscount { get; set; }
        
        public decimal NetAmount { get; set; }
        
        public string CurrencyCode { get; set; }
        
        public string Status { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public bool IsPaid { get; set; }
        
        public bool IsCancelled { get; set; }
        
        public bool IsSuspended { get; set; }
        
        public string Notes { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string ModifiedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
    }
}
