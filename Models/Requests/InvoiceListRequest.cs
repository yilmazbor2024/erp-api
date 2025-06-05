using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Fatura listesi istek modeli
    /// </summary>
    public class InvoiceListRequest
    {
        /// <summary>
        /// Sayfa numarası
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Sıralama alanı
        /// </summary>
        public string SortBy { get; set; } = "invoiceDate";

        /// <summary>
        /// Sıralama yönü
        /// </summary>
        public string SortDirection { get; set; } = "desc";

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Tedarikçi kodu
        /// </summary>
        public string VendorCode { get; set; }

        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Başlangıç tarihi
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Bitiş tarihi
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Fatura durumu (completed, cancelled)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// İşlem kodu (WS: Toptan Satış, BP: Toptan Alış, EP: Masraf Alış, EXP: Masraf Satış)
        /// </summary>
        public string ProcessCode { get; set; }

        /// <summary>
        /// Dil kodu
        /// </summary>
        public string LangCode { get; set; }

        /// <summary>
        /// Şirket kodu
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Mağaza kodu
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Depo kodu
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Tamamlandı mı?
        /// </summary>
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// Askıya alındı mı?
        /// </summary>
        public bool? IsSuspended { get; set; }

        /// <summary>
        /// İade mi?
        /// </summary>
        public bool? IsReturn { get; set; }

        /// <summary>
        /// E-Fatura mı?
        /// </summary>
        public bool? IsEInvoice { get; set; }
    }
} 