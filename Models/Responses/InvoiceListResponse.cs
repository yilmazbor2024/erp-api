using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Fatura listesi yanıt modeli
    /// </summary>
    public class InvoiceResponseList
    {
        /// <summary>
        /// Faturalar
        /// </summary>
        public List<InvoiceResponse> Invoices { get; set; } = new List<InvoiceResponse>();

        /// <summary>
        /// Toplam kayıt sayısı
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Toplam sayfa sayısı
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Mevcut sayfa numarası
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// Fatura özet bilgisi yanıt modeli
    /// </summary>
    public class InvoiceResponse
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
        /// Net toplam tutar
        /// </summary>
        public decimal NetTotal { get; set; }

        /// <summary>
        /// Genel toplam tutar
        /// </summary>
        public decimal GrandTotal { get; set; }

        /// <summary>
        /// Tamamlandı mı?
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// İptal edildi mi?
        /// </summary>
        public bool IsCancelled { get; set; }
    }
} 