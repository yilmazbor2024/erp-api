using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Contact
{
    /// <summary>
    /// Kontak oluşturma isteği modeli
    /// </summary>
    public class ContactCreateRequest
    {
        /// <summary>
        /// Kontak tipi kodu
        /// </summary>
        [Required(ErrorMessage = "Kontak tipi kodu zorunludur")]
        [StringLength(10, ErrorMessage = "Kontak tipi kodu en fazla 10 karakter olabilir")]
        public string ContactTypeCode { get; set; }

        /// <summary>
        /// Kontak adı
        /// </summary>
        [Required(ErrorMessage = "Kontak adı zorunludur")]
        [StringLength(100, ErrorMessage = "Kontak adı en fazla 100 karakter olabilir")]
        public string FirstName { get; set; }

        /// <summary>
        /// Kontak soyadı
        /// </summary>
        [Required(ErrorMessage = "Kontak soyadı zorunludur")]
        [StringLength(100, ErrorMessage = "Kontak soyadı en fazla 100 karakter olabilir")]
        public string LastName { get; set; }

        /// <summary>
        /// Telefon numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// E-posta adresi
        /// </summary>
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }

        /// <summary>
        /// Yetkili mi?
        /// </summary>
        public bool IsAuthorized { get; set; } = false;
    }
}
