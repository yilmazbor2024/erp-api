using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Payment
{
    /// <summary>
    /// Kasa ödeme satırı isteği
    /// </summary>
    public class CashPaymentRowRequest
    {
        /// <summary>
        /// Ödeme satırı ID'si (güncelleme için)
        /// </summary>
        public Guid? CashLineId { get; set; }
        
        /// <summary>
        /// Tutar
        /// </summary>
        [Required(ErrorMessage = "Tutar zorunludur")]
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; } = "TRY";
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Döviz kuru
        /// </summary>
        public float ExchangeRate { get; set; } = 1;
        
        /// <summary>
        /// Kasa hesap kodu
        /// </summary>
        public string CashAccountCode { get; set; }
        
        /// <summary>
        /// Satır işlem tipi (Create, Update, Delete)
        /// </summary>
        public string OperationType { get; set; } = "Create";
    }
}
