using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Müşteri finansal bilgileri güncelleme yanıtı
    /// </summary>
    public class CustomerFinancialUpdateResponse
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mesaj
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Kredi limiti
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Ödeme planı kodu
        /// </summary>
        public string PaymentPlanCode { get; set; }

        /// <summary>
        /// Güncelleme tarihi
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Güncelleyen kullanıcı
        /// </summary>
        public string UpdatedUserName { get; set; }
    }
}
