using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Müşteri kredi limiti ve bakiye bilgilerini içeren yanıt modeli
    /// </summary>
    public class CustomerCreditInfoResponse
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Müşteri açıklaması
        /// </summary>
        public string CustomerDescription { get; set; }

        /// <summary>
        /// Kredi limiti
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Borç toplamı
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// Alacak toplamı
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// Bakiye (Borç - Alacak)
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Açık risk tutarı
        /// </summary>
        public decimal OpenRisk { get; set; }

        /// <summary>
        /// Bakiye ve risk toplamı
        /// </summary>
        public decimal BalanceAndRisk { get; set; }

        /// <summary>
        /// Kalan kredi limiti
        /// </summary>
        public decimal RemainingCreditLimit { get; set; }
    }
}
