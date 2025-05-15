using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Müşteri iletişim kişisi güncelleme isteği modeli
    /// </summary>
    public class CustomerContactUpdateRequestNew
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri kodu zorunludur")]
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// İletişim kişisi ID'si
        /// </summary>
        public Guid ContactID { get; set; }
        
        /// <summary>
        /// İletişim kişisi tipi kodu
        /// </summary>
        [Required(ErrorMessage = "İletişim kişisi tipi kodu zorunludur")]
        [StringLength(10, ErrorMessage = "İletişim kişisi tipi kodu en fazla 10 karakter olabilir")]
        public string ContactTypeCode { get; set; }
        
        /// <summary>
        /// İletişim kişisi adı
        /// </summary>
        [Required(ErrorMessage = "İletişim kişisi adı zorunludur")]
        [StringLength(50, ErrorMessage = "İletişim kişisi adı en fazla 50 karakter olabilir")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// İletişim kişisi soyadı
        /// </summary>
        [Required(ErrorMessage = "İletişim kişisi soyadı zorunludur")]
        [StringLength(50, ErrorMessage = "İletişim kişisi soyadı en fazla 50 karakter olabilir")]
        public string LastName { get; set; }
        
        /// <summary>
        /// Unvan kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Unvan kodu en fazla 10 karakter olabilir")]
        public string TitleCode { get; set; }
        
        /// <summary>
        /// İş unvanı kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "İş unvanı kodu en fazla 10 karakter olabilir")]
        public string JobTitleCode { get; set; }
        
        /// <summary>
        /// Yetkili mi?
        /// </summary>
        public bool IsAuthorized { get; set; }
        
        /// <summary>
        /// Kimlik numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Kimlik numarası en fazla 20 karakter olabilir")]
        public string IdentityNum { get; set; }
        
        /// <summary>
        /// Varsayılan iletişim kişisi mi?
        /// </summary>
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// İletişim kişisi bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}
