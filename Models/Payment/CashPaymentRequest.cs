using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Payment
{
    public class CashPaymentRequest
    {
        [Required]
        public Guid InvoiceHeaderID { get; set; }
        
        [Required]
        public string CurrencyCode { get; set; } = "TRY";
        
        public string Description { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        public string StoreCode { get; set; }
        
        [Required]
        public string CurrAccCode { get; set; }
        
        public byte CurrAccTypeCode { get; set; } = 3; // Varsayılan müşteri tipi
        
        public string OfficeCode { get; set; } = "M";
        
        public string GLTypeCode { get; set; }
        
        public List<CashPaymentAttributeRequest> Attributes { get; set; } = new List<CashPaymentAttributeRequest>();
    }

    public class CashPaymentAttributeRequest
    {
        [Required]
        public string AttributeTypeCode { get; set; }
        
        [Required]
        public string AttributeCode { get; set; }
    }

    public class CashPaymentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? CashHeaderID { get; set; }
        public Guid? PaymentHeaderID { get; set; }
        public string PaymentNumber { get; set; }
        public string RefNumber { get; set; }
    }
}
