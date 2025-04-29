using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    /// <summary>
    /// Yeni müşteri adresi oluşturma isteği modeli
    /// </summary>
    public class CustomerAddressCreateRequestNew
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri kodu zorunludur")]
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Adres ID
        /// </summary>
        public string AddressID { get; set; }

        /// <summary>
        /// Posta Adresi ID (GUID)
        /// </summary>
        public Guid? PostalAddressID { get; set; }

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
        
        /// <summary>
        /// Ülke kodu
        /// </summary>
        [Required(ErrorMessage = "Ülke kodu zorunludur")]
        [StringLength(5, ErrorMessage = "Ülke kodu en fazla 5 karakter olabilir")]
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Eyalet/Bölge kodu
        /// </summary>
        [StringLength(5, ErrorMessage = "Eyalet kodu en fazla 5 karakter olabilir")]
        public string StateCode { get; set; }
        
        /// <summary>
        /// Şehir kodu
        /// </summary>
        [Required(ErrorMessage = "Şehir kodu zorunludur")]
        [StringLength(10, ErrorMessage = "Şehir kodu en fazla 10 karakter olabilir")]
        public string CityCode { get; set; }
        
        /// <summary>
        /// İlçe kodu
        /// </summary>
        [Required(ErrorMessage = "İlçe kodu zorunludur")]
        [StringLength(10, ErrorMessage = "İlçe kodu en fazla 10 karakter olabilir")]
        public string DistrictCode { get; set; }
        
        /// <summary>
        /// Posta kodu
        /// </summary>
        [StringLength(20, ErrorMessage = "Posta kodu en fazla 20 karakter olabilir")]
        public string ZipCode { get; set; }
        
        /// <summary>
        /// Site adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Site adı en fazla 100 karakter olabilir")]
        public string SiteName { get; set; }
        
        /// <summary>
        /// Bina adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Bina adı en fazla 100 karakter olabilir")]
        public string BuildingName { get; set; }
        
        /// <summary>
        /// Bina numarası
        /// </summary>
        [StringLength(10, ErrorMessage = "Bina numarası en fazla 10 karakter olabilir")]
        public string BuildingNum { get; set; }
        
        /// <summary>
        /// Kat numarası
        /// </summary>
        public short? FloorNum { get; set; }
        
        /// <summary>
        /// Kapı numarası
        /// </summary>
        public short? DoorNum { get; set; }
        
        /// <summary>
        /// Mahalle kodu
        /// </summary>
        public int? QuarterCode { get; set; }
        
        /// <summary>
        /// Mahalle adı
        /// </summary>
        [StringLength(100, ErrorMessage = "Mahalle adı en fazla 100 karakter olabilir")]
        public string QuarterName { get; set; }
        
        /// <summary>
        /// Cadde
        /// </summary>
        [StringLength(100, ErrorMessage = "Cadde en fazla 100 karakter olabilir")]
        public string Street { get; set; }
        
        /// <summary>
        /// Sokak kodu
        /// </summary>
        public int? StreetCode { get; set; }
        
        /// <summary>
        /// Yol tarifi
        /// </summary>
        [StringLength(500, ErrorMessage = "Yol tarifi en fazla 500 karakter olabilir")]
        public string DrivingDirections { get; set; }
        
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
        /// Fatura adresi mi?
        /// </summary>
        public bool IsBillingAddress { get; set; }

        /// <summary>
        /// Sevkiyat adresi mi?
        /// </summary>
        public bool IsShippingAddress { get; set; }

        /// <summary>
        /// Varsayılan adres mi?
        /// </summary>
        public bool IsDefault { get; set; }
        
        /// <summary>
        /// Adres bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}
