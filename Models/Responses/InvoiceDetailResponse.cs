using System;
using System.Collections.Generic;

namespace erp_api.Models.Responses
{
    /// <summary>
    /// Fatura detay yanıt modeli
    /// </summary>
    public class InvoiceDetailResponse
    {
        /// <summary>
        /// Fatura ID
        /// </summary>
        public string InvoiceHeaderId { get; set; }

        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Fatura tarihi
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Fatura saati
        /// </summary>
        public TimeSpan InvoiceTime { get; set; }

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CurrAccCode { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CurrAccDesc { get; set; }

        /// <summary>
        /// Ofis kodu
        /// </summary>
        public string OfficeCode { get; set; }

        /// <summary>
        /// Açıklama 1
        /// </summary>
        public string Description1 { get; set; }

        /// <summary>
        /// Açıklama 2
        /// </summary>
        public string Description2 { get; set; }

        /// <summary>
        /// Açıklama 3
        /// </summary>
        public string Description3 { get; set; }

        /// <summary>
        /// Açıklama 4
        /// </summary>
        public string Description4 { get; set; }

        /// <summary>
        /// Net toplam tutar
        /// </summary>
        public decimal NetTotal { get; set; }

        /// <summary>
        /// Vergi toplamı
        /// </summary>
        public decimal TaxTotal { get; set; }

        /// <summary>
        /// Genel toplam tutar
        /// </summary>
        public decimal GrandTotal { get; set; }

        /// <summary>
        /// Ödeme planı kodu
        /// </summary>
        public string PaymentPlanCode { get; set; }

        /// <summary>
        /// Tamamlandı mı?
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// İptal edildi mi?
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Belge takip numarası
        /// </summary>
        public string DocTrackingNumber { get; set; }

        /// <summary>
        /// Fatura satırları
        /// </summary>
        public List<InvoiceLineResponse> Lines { get; set; } = new List<InvoiceLineResponse>();
    }

    /// <summary>
    /// Fatura satırı yanıt modeli
    /// </summary>
    public class InvoiceLineResponse
    {
        /// <summary>
        /// Fatura satırı ID
        /// </summary>
        public string InvoiceLineId { get; set; }

        /// <summary>
        /// Fatura ID
        /// </summary>
        public string InvoiceHeaderId { get; set; }

        /// <summary>
        /// Satır numarası
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Ürün kodu
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Ürün adı
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Miktar
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Birim kodu
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// Birim fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Net tutar
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// Vergi oranı
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// Vergi tutarı
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Toplam tutar
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// İndirim oranı
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
    }
} 