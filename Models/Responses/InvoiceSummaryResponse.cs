using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Fatura tiplerine göre toplam tutarları içeren yanıt modeli
    /// </summary>
    public class InvoiceTypeSummaryResponse
    {
        public string InvoiceTypeCode { get; set; }
        public string InvoiceTypeDescription { get; set; }
        public int InvoiceCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalVatAmount { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public string CurrencyCode { get; set; }
    }

    /// <summary>
    /// Tarih aralığına göre fatura toplamlarını içeren yanıt modeli
    /// </summary>
    public class InvoiceDateSummaryResponse
    {
        public DateTime InvoiceDate { get; set; }
        public int InvoiceCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalVatAmount { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public string CurrencyCode { get; set; }
    }

    /// <summary>
    /// Müşterilere göre fatura toplamlarını içeren yanıt modeli
    /// </summary>
    public class InvoiceCustomerSummaryResponse
    {
        public string CustomerCode { get; set; }
        public string CustomerDescription { get; set; }
        public int InvoiceCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalVatAmount { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public string CurrencyCode { get; set; }
    }

    /// <summary>
    /// Fatura özet bilgilerini içeren ana yanıt modeli
    /// </summary>
    public class InvoiceSummaryListResponse
    {
        public List<InvoiceTypeSummaryResponse> InvoiceTypeSummaries { get; set; }
        public List<InvoiceDateSummaryResponse> InvoiceDateSummaries { get; set; }
        public List<InvoiceCustomerSummaryResponse> InvoiceCustomerSummaries { get; set; }
        public int TotalCount { get; set; }
    }
}
