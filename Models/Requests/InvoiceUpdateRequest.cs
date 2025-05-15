using System;

namespace ErpMobile.Api.Models.Requests
{
    public class InvoiceUpdateRequest
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string CurrAccCode { get; set; }
        public int? CurrAccTypeCode { get; set; }
        public string DocCurrencyCode { get; set; }
        public string Notes { get; set; }
        public string InvoiceTypeCode { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsSuspended { get; set; }
    }
}
