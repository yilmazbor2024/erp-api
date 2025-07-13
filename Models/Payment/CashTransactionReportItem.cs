using System;

namespace ErpMobile.Api.Models.Payment
{
    public class CashTransactionReportItem
    {
        /// <summary>
        /// İşlem numarası
        /// </summary>
        public string TransactionNumber { get; set; }
        
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime DocumentDate { get; set; }
        
        /// <summary>
        /// İşlem tipi (Tahsilat, Tediye, Virman)
        /// </summary>
        public string TransactionType { get; set; }
        
        /// <summary>
        /// Kasa kodu
        /// </summary>
        public string CashAccountCode { get; set; }
        
        /// <summary>
        /// Kasa adı
        /// </summary>
        public string CashAccountName { get; set; }
        
        /// <summary>
        /// Karşı kasa kodu (virman işlemlerinde)
        /// </summary>
        public string CounterCashAccountCode { get; set; }
        
        /// <summary>
        /// Karşı kasa adı (virman işlemlerinde)
        /// </summary>
        public string CounterCashAccountName { get; set; }
        
        /// <summary>
        /// Cari hesap kodu
        /// </summary>
        public string CurrAccCode { get; set; }
        
        /// <summary>
        /// Cari hesap adı
        /// </summary>
        public string CurrAccDesc { get; set; }
        
        /// <summary>
        /// Para birimi
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string CreatedBy { get; set; }
        
        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
