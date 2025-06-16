using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Token ile müşteri adres bilgisi ekleme yanıtı
    /// </summary>
    public class CustomerAddressTokenResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Adres ID
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
    }

    /// <summary>
    /// Token ile müşteri iletişim bilgisi ekleme yanıtı
    /// </summary>
    public class CustomerCommunicationTokenResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// İletişim ID
        /// </summary>
        public Guid CommunicationId { get; set; }

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
    }

    /// <summary>
    /// Token ile müşteri contact bilgisi ekleme yanıtı
    /// </summary>
    public class CustomerContactTokenResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        public Guid ContactId { get; set; }

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }
    }
}
