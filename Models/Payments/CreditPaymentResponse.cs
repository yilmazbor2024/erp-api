using System;

namespace ErpApi.Models.Payments
{
    /// <summary>
    /// Vadeli ödeme yanıt modeli
    /// </summary>
    public class CreditPaymentResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// İşlem mesajı
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Oluşturulan borç başlık ID'si
        /// </summary>
        public string DebitHeaderId { get; set; }
    }
}
