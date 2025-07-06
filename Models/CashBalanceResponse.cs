using System;
using System.Collections.Generic;

namespace ErpMobile.Api.Models
{
    /// <summary>
    /// Kasa bakiye bilgilerini içeren yanıt modeli
    /// </summary>
    public class CashBalanceResponse
    {
        /// <summary>
        /// Kasa kodu
        /// </summary>
        public string CashAccountCode { get; set; }
        
        /// <summary>
        /// Kasa adı
        /// </summary>
        public string CashAccountName { get; set; }
        
        /// <summary>
        /// Yerel para birimi kodu (TRY)
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Yerel para birimi başlangıç bakiyesi
        /// </summary>
        public decimal OpeningBalance { get; set; }
        
        /// <summary>
        /// Yerel para birimi toplam tahsilat tutarı
        /// </summary>
        public decimal TotalReceipts { get; set; }
        
        /// <summary>
        /// Yerel para birimi toplam ödeme tutarı
        /// </summary>
        public decimal TotalPayments { get; set; }
        
        /// <summary>
        /// Yerel para birimi toplam gelen virman tutarı
        /// </summary>
        public decimal TotalIncomingTransfers { get; set; }
        
        /// <summary>
        /// Yerel para birimi toplam giden virman tutarı
        /// </summary>
        public decimal TotalOutgoingTransfers { get; set; }
        
        /// <summary>
        /// Yerel para birimi kapanış bakiyesi
        /// </summary>
        public decimal ClosingBalance { get; set; }
        
        /// <summary>
        /// Döviz bakiyeleri listesi (USD, EUR vb. para birimleri için)
        /// </summary>
        public List<CurrencyBalance> ForeignCurrencyBalances { get; set; } = new List<CurrencyBalance>();
        
        /// <summary>
        /// Kasa hareketleri
        /// </summary>
        public List<CashTransactionSummary> Transactions { get; set; }
    }
    
    /// <summary>
    /// Kasa işlem özeti
    /// </summary>
    public class CashTransactionSummary
    {
        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime DocumentDate { get; set; }
        
        /// <summary>
        /// İşlem numarası
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// İşlem türü kodu
        /// </summary>
        public string CashTransTypeCode { get; set; }
        
        /// <summary>
        /// İşlem türü açıklaması
        /// </summary>
        public string CashTransTypeDescription { get; set; }
        
        /// <summary>
        /// Fiş numarası
        /// </summary>
        public string CashTransNumber { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Yerel para birimi kodu (TRY)
        /// </summary>
        public string Loc_CurrencyCode { get; set; }
        
        /// <summary>
        /// Yerel para birimi borç tutarı
        /// </summary>
        public decimal Loc_Debit { get; set; }
        
        /// <summary>
        /// Yerel para birimi alacak tutarı
        /// </summary>
        public decimal Loc_Credit { get; set; }
        
        /// <summary>
        /// Yerel para birimi bakiye
        /// </summary>
        public decimal Loc_Balance { get; set; }
        
        /// <summary>
        /// Döviz para birimi kodu (USD, EUR vb.)
        /// </summary>
        public string Doc_CurrencyCode { get; set; }
        
        /// <summary>
        /// Döviz borç tutarı
        /// </summary>
        public decimal Doc_Debit { get; set; }
        
        /// <summary>
        /// Döviz alacak tutarı
        /// </summary>
        public decimal Doc_Credit { get; set; }
        
        /// <summary>
        /// Döviz bakiye
        /// </summary>
        public decimal Doc_Balance { get; set; }
        
        /// <summary>
        /// Kaynak kasa kodu (virman işlemleri için)
        /// </summary>
        public string SourceCashAccountCode { get; set; }
        
        /// <summary>
        /// Hedef kasa kodu (virman işlemleri için)
        /// </summary>
        public string TargetCashAccountCode { get; set; }
        
        /// <summary>
        /// Cari hesap kodu
        /// </summary>
        public string CurrAccCode { get; set; }
        
        /// <summary>
        /// Cari hesap açıklaması
        /// </summary>
        public string CurrAccDescription { get; set; }
    }
}
