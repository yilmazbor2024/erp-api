using System;

namespace ErpMobile.Api.Models.Payment
{
    public class CashTransactionReportRequest
    {
        /// <summary>
        /// Başlangıç tarihi (yyyyMMdd formatında)
        /// </summary>
        public string StartDate { get; set; } = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
        
        /// <summary>
        /// Bitiş tarihi (yyyyMMdd formatında)
        /// </summary>
        public string EndDate { get; set; } = DateTime.Now.ToString("yyyyMMdd");
        
        /// <summary>
        /// Kasa hesap kodu
        /// </summary>
        public string CashAccountCode { get; set; }
        
        /// <summary>
        /// İşlem tipi (tahsilat, tediye, virman)
        /// </summary>
        public string TransactionType { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string CurrencyCode { get; set; }
        
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
        public string SortField { get; set; } = "DocumentDate";
        
        /// <summary>
        /// Sıralama yönü (ASC, DESC)
        /// </summary>
        public string SortOrder { get; set; } = "DESC";
    }
}
