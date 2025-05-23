using System;

namespace ErpMobile.Api.Models.Invoice
{
    public class InvoicePaymentDetailModel
    {
        public string DebitLineID { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public string RelationCurrencyCode { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
