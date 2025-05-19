using System;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Müşteri adres güncelleme isteği modeli
    /// </summary>
    public class CustomerAddressUpdateRequestNew
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri kodu zorunludur")]
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Posta adresi ID'si
        /// </summary>
        public Guid PostalAddressID { get; set; }
        
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
        [StringLength(1000, ErrorMessage = "Adres en fazla 1000 karakter olabilir")]
        public string Address { get; set; }
        
        /// Ülke kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Ülke kodu en fazla 10 karakter olabilir")]
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Eyalet/Bölge kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Eyalet/Bölge kodu en fazla 10 karakter olabilir")]
        public string StateCode { get; set; }
        
        /// <summary>
        /// Şehir kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Şehir kodu en fazla 10 karakter olabilir")]
        public string CityCode { get; set; }
        
        /// <summary>
        /// İlçe kodu
        /// </summary>
        [StringLength(30, ErrorMessage = "İlçe kodu en fazla 30 karakter olabilir")]
        public string DistrictCode { get; set; }
        
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
        /// Adres bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; }
        
        /// <summary>
        /// Varsayılan adres mi?
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Adres ID'si
        /// </summary>
        public string AddressID { get; set; }
        

    }
}
