using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    public class CustomerAddressRequest
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>
        /// Adres ID
        /// </summary>
        public int AddressID { get; set; } = 0;

        /// <summary>
        /// Adres tipi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Adres tipi kodu en fazla 10 karakter olabilir")]
        public string AddressTypeCode { get; set; } = string.Empty;

        /// <summary>
        /// Adres
        /// </summary>
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Ülke kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Ülke kodu en fazla 10 karakter olabilir")]
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// İl kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "İl kodu en fazla 10 karakter olabilir")]
        public string StateCode { get; set; } = string.Empty;

        /// <summary>
        /// Şehir kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Şehir kodu en fazla 10 karakter olabilir")]
        public string CityCode { get; set; } = string.Empty;

        /// <summary>
        /// İlçe kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "İlçe kodu en fazla 10 karakter olabilir")]
        public string DistrictCode { get; set; } = string.Empty;
        
        /// <summary>
        /// Kasaba/Belde kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Kasaba/Belde kodu en fazla 10 karakter olabilir")]
        public string TownCode { get; set; } = string.Empty;
        
        /// <summary>
        /// Posta kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Posta kodu en fazla 10 karakter olabilir")]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Vergi dairesi
        /// </summary>
        [StringLength(100, ErrorMessage = "Vergi dairesi en fazla 100 karakter olabilir")]
        public string TaxOffice { get; set; } = string.Empty;

        /// <summary>
        /// Vergi dairesi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Vergi dairesi kodu en fazla 10 karakter olabilir")]
        public string TaxOfficeCode { get; set; } = string.Empty;

        /// <summary>
        /// Vergi numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Vergi numarası en fazla 20 karakter olabilir")]
        public string TaxNumber { get; set; } = string.Empty;

        /// <summary>
        /// Bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; } = false;

        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Oluşturan kullanıcı adı en fazla 50 karakter olabilir")]
        public string CreatedUserName { get; set; } = string.Empty;

        /// <summary>
        /// Son güncelleyen kullanıcı adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Son güncelleyen kullanıcı adı en fazla 50 karakter olabilir")]
        public string LastUpdatedUserName { get; set; } = string.Empty;

        /// <summary>
        /// Varsayılan adres mi?
        /// </summary>
        public bool IsDefault { get; set; } = false;
    }
}
