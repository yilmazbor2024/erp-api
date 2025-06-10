using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Payment
{
    public class CashPaymentRequest
{
    public Guid InvoiceId { get; set; }
    public string CurrAccCode { get; set; }
    public DateTime DocumentDate { get; set; }
    public string InvoiceNumber { get; set; } // Fatura numarası
    public string Description { get; set; }
    public string CashCurrAccCode { get; set; } = "101"; // Varsayılan kasa kodu
    public string DocCurrencyCode { get; set; } = "TRY"; // Varsayılan para birimi
    public List<PaymentRow> PaymentRows { get; set; } = new List<PaymentRow>();
    public List<CashPaymentAttributeRequest> Attributes { get; set; } = new List<CashPaymentAttributeRequest>();
}

public class PaymentRow
{
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; } = "TRY";
    public string Description { get; set; }
    public float ExchangeRate { get; set; } = 1;
}

public class CashPaymentAttributeRequest
{
    public string AttributeTypeCode { get; set; }
    public string AttributeCode { get; set; }
    public string AttributeValue { get; set; }
}

    public class CashPaymentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? CashHeaderId { get; set; }
        public Guid? PaymentHeaderId { get; set; }
        public string PaymentNumber { get; set; }
        public string RefNumber { get; set; }
    }
}
