using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Müşteri güncelleme yanıtı
    /// </summary>
    public class CustomerUpdateResponse
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Güncellenme tarihi
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// İşlem mesajı
        /// </summary>
        public string Message { get; set; }
    }
}
