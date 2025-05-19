using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Yeni müşteri iletişim kişisi oluşturma isteği modeli
    /// </summary>
    public class CustomerContactCreateRequestNew
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// İletişim kişisi ID'si
        /// </summary>
        public Guid? ContactID { get; set; }
        
        /// <summary>
        /// İletişim kişisi tipi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "İletişim kişisi tipi kodu en fazla 10 karakter olabilir")]
        public string ContactTypeCode { get; set; }
        
        /// <summary>
        /// İletişim kişisi adı
        /// </summary>
        [StringLength(50, ErrorMessage = "İletişim kişisi adı en fazla 50 karakter olabilir")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// İletişim kişisi soyadı
        /// </summary>
        [StringLength(50, ErrorMessage = "İletişim kişisi soyadı en fazla 50 karakter olabilir")]
        public string LastName { get; set; }
        

        /// <summary>
        /// Yetkili mi?
        /// </summary>
        public bool IsAuthorized { get; set; }
        
        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        public string CreatedUserName { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı adı
        /// </summary>
        public string LastUpdatedUserName { get; set; }

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
