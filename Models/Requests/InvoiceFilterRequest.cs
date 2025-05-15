using System;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Fatura filtreleme istek modeli
    /// </summary>
    public class InvoiceFilterRequest
    {
        /// <summary>
        /// Sayfa numarası
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Sıralama alanı
        /// </summary>
        public string OrderBy { get; set; } = "InvoiceDate DESC";

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Arama metni (fatura numarası veya müşteri adı)
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Başlangıç tarihi
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Bitiş tarihi
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
} 