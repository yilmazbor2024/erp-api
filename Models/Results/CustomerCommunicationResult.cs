using System;

namespace ErpMobile.Api.Models.Results
{
    /// <summary>
    /// Müşteri iletişim bilgisi kaydetme işlemi sonucu
    /// </summary>
    public class CustomerCommunicationResult
    {
        /// <summary>
        /// İşlemin başarılı olup olmadığı
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Kaydedilen iletişim bilgisinin ID'si
        /// </summary>
        public Guid CommunicationId { get; set; }

        /// <summary>
        /// Hata durumunda hata mesajı
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
