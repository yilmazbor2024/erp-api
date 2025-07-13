using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ErpMobile.Api.Models.Payment;

namespace ErpMobile.Api.Models
{
    /// <summary>
    /// Kasa hareketi güncelleme isteği
    /// </summary>
    public class CashUpdateRequest
    {
        /// <summary>
        /// Kasa başlık ID'si
        /// </summary>
        [Required(ErrorMessage = "Kasa başlık ID'si zorunludur")]
        public string CashHeaderId { get; set; }
        
        /// <summary>
        /// Kasa hareket numarası
        /// </summary>
        public string CashTransNumber { get; set; }
        
        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Toplam tutar
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Ödeme satırları
        /// </summary>
        public List<CashPaymentRowRequest> PaymentRows { get; set; }
    }
}
