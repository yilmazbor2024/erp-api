using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Fatura oluşturma istek modeli
    /// </summary>
    public class InvoiceCreateRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Fatura tarihi
        /// </summary>
        [Required]
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Fatura saati
        /// </summary>
        public TimeSpan? InvoiceTime { get; set; }

        /// <summary>
        /// Ofis kodu
        /// </summary>
        [Required]
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
        /// Ödeme planı kodu
        /// </summary>
        public string PaymentPlanCode { get; set; }

        /// <summary>
        /// Fatura satırları
        /// </summary>
        [Required]
        public List<InvoiceLineCreateRequest> Lines { get; set; } = new List<InvoiceLineCreateRequest>();
    }

    /// <summary>
    /// Fatura satırı oluşturma istek modeli
    /// </summary>
    public class InvoiceLineCreateRequest
    {
        /// <summary>
        /// Ürün kodu
        /// </summary>
        [Required]
        public string ItemCode { get; set; }

        /// <summary>
        /// Miktar
        /// </summary>
        [Required]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Birim kodu
        /// </summary>
        [Required]
        public string UnitCode { get; set; }

        /// <summary>
        /// Birim fiyat
        /// </summary>
        [Required]
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