using System;

namespace ErpMobile.Api.Models.Results
{
    /// <summary>
    /// Müşteri adres kaydetme işlemi sonucu
    /// </summary>
    public class CustomerAddressResult
    {
        /// <summary>
        /// İşlemin başarılı olup olmadığı
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Kaydedilen adresin ID'si
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Hata durumunda hata mesajı
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
