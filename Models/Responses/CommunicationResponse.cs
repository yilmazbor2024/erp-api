using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// İletişim bilgisi yanıt modeli
    /// </summary>
    public class CommunicationResponse
    {
        /// <summary>
        /// İletişim ID
        /// </summary>
        public int CommunicationID { get; set; }

        /// <summary>
        /// İletişim tipi kodu
        /// </summary>
        public string CommunicationTypeCode { get; set; }

        /// <summary>
        /// İletişim tipi açıklaması
        /// </summary>
        public string CommunicationTypeDescription { get; set; }

        /// <summary>
        /// İletişim adresi (telefon, e-posta vb.)
        /// </summary>
        public string CommAddress { get; set; }

        /// <summary>
        /// Kontak ID
        /// </summary>
        public int? ContactID { get; set; }

        /// <summary>
        /// Kontak adı
        /// </summary>
        public string ContactFirstName { get; set; }

        /// <summary>
        /// Kontak soyadı
        /// </summary>
        public string ContactLastName { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string CreatedUserName { get; set; }
    }
}
