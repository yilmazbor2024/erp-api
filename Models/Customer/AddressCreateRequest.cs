using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Customer
{
    /// <summary>
    /// Adres oluşturma isteği modeli
    /// </summary>
    public class AddressCreateRequest
    {
        /// <summary>
        /// Adres tipi kodu
        /// </summary>
        [Required(ErrorMessage = "Adres tipi kodu zorunludur")]
        [StringLength(10, ErrorMessage = "Adres tipi kodu en fazla 10 karakter olabilir")]
        public string AddressTypeCode { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        [Required(ErrorMessage = "Adres zorunludur")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string Address { get; set; }

        /// <summary>
        /// Şehir kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Şehir kodu en fazla 10 karakter olabilir")]
        public string CityCode { get; set; }

        /// <summary>
        /// İlçe kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "İlçe kodu en fazla 10 karakter olabilir")]
        public string DistrictCode { get; set; }

        /// <summary>
        /// Posta kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Posta kodu en fazla 10 karakter olabilir")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Ülke kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Ülke kodu en fazla 10 karakter olabilir")]
        public string CountryCode { get; set; } = "TR";

        /// <summary>
        /// Vergi dairesi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Vergi dairesi kodu en fazla 10 karakter olabilir")]
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// Vergi numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Vergi numarası en fazla 20 karakter olabilir")]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Varsayılan adres mi?
        /// </summary>
        public bool IsDefault { get; set; } = false;
    }
}
