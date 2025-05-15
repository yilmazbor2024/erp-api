using System.Collections.Generic;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Ödeme planları listesi yanıt modeli
    /// </summary>
    public class PaymentPlanListResponse
    {
        /// <summary>
        /// Ödeme planları
        /// </summary>
        public List<PaymentPlanResponse> PaymentPlans { get; set; } = new List<PaymentPlanResponse>();
    }

    /// <summary>
    /// Ödeme planı yanıt modeli
    /// </summary>
    public class PaymentPlanResponse
    {
        /// <summary>
        /// Ödeme planı kodu
        /// </summary>
        public string PaymentPlanCode { get; set; }

        /// <summary>
        /// Ödeme planı adı
        /// </summary>
        public string PaymentPlanDesc { get; set; }

        /// <summary>
        /// Kredi kartı planı mı?
        /// </summary>
        public bool IsCreditCardPlan { get; set; }

        /// <summary>
        /// Blokeli mi?
        /// </summary>
        public bool IsBlocked { get; set; }
    }
} 