using System;

namespace ErpMobile.Api.Models
{
    /// <summary>
    /// Döviz bakiye bilgilerini içeren model
    /// </summary>
    public class CurrencyBalance
    {
        /// <summary>
        /// Para birimi kodu (USD, EUR vb.)
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Başlangıç bakiyesi
        /// </summary>
        public decimal OpeningBalance { get; set; }
        
        /// <summary>
        /// Toplam tahsilat tutarı
        /// </summary>
        public decimal TotalReceipts { get; set; }
        
        /// <summary>
        /// Toplam ödeme tutarı
        /// </summary>
        public decimal TotalPayments { get; set; }
        
        /// <summary>
        /// Toplam gelen virman tutarı
        /// </summary>
        public decimal TotalIncomingTransfers { get; set; }
        
        /// <summary>
        /// Toplam giden virman tutarı
        /// </summary>
        public decimal TotalOutgoingTransfers { get; set; }
        
        /// <summary>
        /// Kapanış bakiyesi
        /// </summary>
        public decimal ClosingBalance { get; set; }
    }
}
