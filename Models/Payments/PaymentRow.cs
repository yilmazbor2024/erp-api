using System;

namespace ErpApi.Models.Payments
{
    /// <summary>
    /// Ödeme satırı modeli
    /// </summary>
    public class PaymentRow
    {
        /// <summary>
        /// Ödeme tutarı
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Vade tarihi
        /// </summary>
        public DateTime DueDate { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Döviz kuru
        /// </summary>
        public decimal ExchangeRate { get; set; } = 1m;
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
    }
}
