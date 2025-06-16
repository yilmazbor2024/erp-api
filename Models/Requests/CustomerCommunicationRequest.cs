using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    public class CustomerCommunicationRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>
        /// İletişim tipi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "İletişim tipi kodu en fazla 10 karakter olabilir")]
        public string CommunicationTypeCode { get; set; } = string.Empty;

        /// <summary>
        /// İletişim bilgisi (telefon, e-posta vb.)
        /// </summary>
        [StringLength(100, ErrorMessage = "İletişim bilgisi en fazla 100 karakter olabilir")]
        public string CommunicationValue { get; set; } = string.Empty;

        /// <summary>
        /// Varsayılan iletişim bilgisi mi?
        /// </summary>
        public bool IsDefault { get; set; } = false;
        
        /// <summary>
        /// Reklam gönderilebilir mi?
        /// </summary>
        public bool CanSendAdvert { get; set; } = false;
        
        /// <summary>
        /// Onaylanmış mı?
        /// </summary>
        public bool IsConfirmed { get; set; } = false;
        
        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        public string CreatedUserName { get; set; } = string.Empty;
        
        /// <summary>
        /// Son güncelleyen kullanıcı adı
        /// </summary>
        public string LastUpdatedUserName { get; set; } = string.Empty;
    }
}
