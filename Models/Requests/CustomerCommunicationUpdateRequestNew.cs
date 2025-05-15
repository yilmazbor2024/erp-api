using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Müşteri iletişim bilgisi güncelleme isteği modeli
    /// </summary>
    public class CustomerCommunicationUpdateRequestNew
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri kodu zorunludur")]
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// İletişim bilgisi ID'si
        /// </summary>
        public Guid CommunicationID { get; set; }

        /// <summary>
        /// İletişim tipi kodu
        /// </summary>
        [Required(ErrorMessage = "İletişim tipi zorunludur")]
        [StringLength(10, ErrorMessage = "İletişim tipi kodu en fazla 10 karakter olabilir")]
        public string CommunicationTypeCode { get; set; }
        
        /// <summary>
        /// İletişim adresi (e-posta, telefon numarası vb.)
        /// </summary>
        [Required(ErrorMessage = "İletişim adresi zorunludur")]
        [StringLength(100, ErrorMessage = "İletişim adresi en fazla 100 karakter olabilir")]
        public string CommAddress { get; set; }
        
        /// <summary>
        /// Reklam gönderimi yapılabilir mi?
        /// </summary>
        public bool CanSendAdvert { get; set; }

        /// <summary>
        /// İletişim bilgisi engellenmiş mi?
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// İletişim bilgisi doğrulanmış mı?
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Varsayılan iletişim bilgisi mi?
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
