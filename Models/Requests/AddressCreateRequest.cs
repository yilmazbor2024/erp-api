using System;
using System.ComponentModel.DataAnnotations;

namespace ErpAPI.Models.Requests
{
    public class AddressCreateRequest
    {
        [Required(ErrorMessage = "Adres tipi gereklidir.")]
        [StringLength(50, ErrorMessage = "Adres tipi kodu 50 karakterden uzun olamaz.")]
        public string AddressTypeCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres gereklidir.")]
        [StringLength(100, ErrorMessage = "Adres 100 karakterden uzun olamaz.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şehir gereklidir.")]
        [StringLength(50, ErrorMessage = "Şehir 50 karakterden uzun olamaz.")]
        public string City { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "İlçe 50 karakterden uzun olamaz.")]
        public string District { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Ülke 50 karakterden uzun olamaz.")]
        public string Country { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Posta kodu 20 karakterden uzun olamaz.")]
        public string PostalCode { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Ülke kodu 50 karakterden uzun olamaz.")]
        public string CountryCode { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "İl kodu 50 karakterden uzun olamaz.")]
        public string StateCode { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Şehir kodu 50 karakterden uzun olamaz.")]
        public string CityCode { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "İlçe kodu 50 karakterden uzun olamaz.")]
        public string DistrictCode { get; set; } = string.Empty;

        public bool IsDefault { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public bool IsBlocked { get; set; } = false;

        // Service Fields
        public string RecordStatus { get; set; } = "A";
        public Guid ServicePassportID { get; set; } = Guid.NewGuid();
        public int CCTID { get; set; } = 0;
        public DateTime RecordDate { get; set; } = DateTime.Now;
    }
} 