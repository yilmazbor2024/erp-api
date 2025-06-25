using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Müşteri adresi oluşturma isteği modeli - prCurrAccPostalAddress tablosuyla %100 uyumlu
    /// </summary>
    public class CustomerAddressCreateRequest
    {
        /// <summary>
        /// Müşteri kodu - prCurrAccPostalAddress.CurrAccCode alanı ile eşleşir
        /// Kullanım amacı: Adresin hangi müşteriye ait olduğunu belirtir
        /// Örnek değer: "121.726"
        /// </summary>
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Adres ID - prCurrAccPostalAddress.AddressID alanı ile eşleşir
        /// Kullanım amacı: Adres kaydının benzersiz kimliği
        /// Veritabanında NOT NULL, varsayılan değer: 0
        /// </summary>
        public long AddressID { get; set; }

        /// <summary>
        /// Adres tipi kodu - prCurrAccPostalAddress.AddressTypeCode alanı ile eşleşir
        /// Kullanım amacı: Adresin tipini belirtir (örn. "2" = "İş Adresi")
        /// cdAddressType tablosu ile ilişkilidir (Foreign Key)
        /// </summary>
        [StringLength(10, ErrorMessage = "Adres tipi kodu en fazla 10 karakter olabilir")]
        public string AddressTypeCode { get; set; }
        
        /// <summary>
        /// Adres - prCurrAccPostalAddress.Address alanı ile eşleşir
        /// Kullanım amacı: Müşterinin açık adres bilgisini içerir
        /// Veritabanında NOT NULL, boş string olabilir
        /// </summary>
        [StringLength(1000, ErrorMessage = "Adres en fazla 1000 karakter olabilir")]
        public string Address { get; set; }
        

 
        /// <summary>
        /// Ülke kodu - prCurrAccPostalAddress.CountryCode alanı ile eşleşir
        /// Kullanım amacı: Adresin bulunduğu ülkenin kodunu belirtir
        /// Veritabanında NOT NULL, varsayılan değer: "TR" (Türkiye)
        /// cdCountry tablosu ile ilişkilidir (Foreign Key)
        /// </summary>
        [StringLength(10, ErrorMessage = "Ülke kodu en fazla 10 karakter olabilir")]
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Eyalet/Bölge kodu - prCurrAccPostalAddress.StateCode alanı ile eşleşir
        /// Kullanım amacı: Adresin bulunduğu eyalet veya bölgenin kodunu belirtir
        /// Veritabanında NOT NULL, varsayılan değer: "TR.00"
        /// cdState tablosu ile ilişkilidir (Foreign Key)
        /// </summary>
        [StringLength(10, ErrorMessage = "Eyalet/Bölge kodu en fazla 10 karakter olabilir")]
        public string StateCode { get; set; }
        
        /// <summary>
        /// Şehir kodu - prCurrAccPostalAddress.CityCode alanı ile eşleşir
        /// Kullanım amacı: Adresin bulunduğu şehrin kodunu belirtir
        /// Veritabanında NOT NULL, varsayılan değer: "TR.00"
        /// cdCity tablosu ile ilişkilidir (Foreign Key)
        /// </summary>
        [StringLength(10, ErrorMessage = "Şehir kodu en fazla 10 karakter olabilir")]
        public string CityCode { get; set; }
        
        /// <summary>
        /// İlçe kodu - prCurrAccPostalAddress.DistrictCode alanı ile eşleşir
        /// Kullanım amacı: Adresin bulunduğu ilçenin kodunu belirtir
        /// Veritabanında NOT NULL, varsayılan değer: boş string
        /// cdDistrict tablosu ile ilişkilidir (Foreign Key)
        /// </summary>
        [StringLength(30, ErrorMessage = "İlçe kodu en fazla 30 karakter olabilir")]
        public string DistrictCode { get; set; }
        
 
        /// <summary>
        /// Vergi dairesi kodu - prCurrAccPostalAddress.TaxOfficeCode alanı ile eşleşir
        /// Kullanım amacı: Müşterinin bağlı olduğu vergi dairesinin kodunu belirtir
        /// Veritabanında NOT NULL, varsayılan değer: boş string
        /// cdTaxOffice tablosu ile ilişkilidir (Foreign Key)
        /// </summary>
        [StringLength(10, ErrorMessage = "Vergi dairesi kodu en fazla 10 karakter olabilir")]
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// Vergi dairesi adı - prCurrAccPostalAddress.TaxOffice alanı ile eşleşir
        /// Kullanım amacı: Müşterinin bağlı olduğu vergi dairesinin adını belirtir (TaxOfficeCode ile aynı amaca hizmet eder)
        /// Veritabanında NOT NULL, varsayılan değer: boş string
        /// </summary>
        [StringLength(30, ErrorMessage = "Vergi dairesi adı en fazla 30 karakter olabilir")]
        public string TaxOffice { get; set; }
        
        /// <summary>
        /// Vergi numarası - prCurrAccPostalAddress.TaxNumber alanı ile eşleşir
        /// Kullanım amacı: Müşterinin vergi numarasını belirtir
        /// Veritabanında NOT NULL, varsayılan değer: boş string
        /// </summary>
        [StringLength(20, ErrorMessage = "Vergi numarası en fazla 20 karakter olabilir")]
        public string TaxNumber { get; set; }
        
        /// <summary>
        /// Posta kodu - prCurrAccAddress.PostalCode alanı ile eşleşir
        /// Kullanım amacı: Adresin posta kodunu belirtir
        /// Veritabanında NOT NULL, varsayılan değer: boş string
        /// </summary>
        [StringLength(20, ErrorMessage = "Posta kodu en fazla 20 karakter olabilir")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Adres bloke mi? - prCurrAccPostalAddress.IsBlocked alanı ile eşleşir
        /// Kullanım amacı: Adresin bloke edilip edilmediğini belirtir
        /// Veritabanında NOT NULL, varsayılan değer: false
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı adı - prCurrAccPostalAddress.CreatedUserName alanı ile eşleşir
        /// Kullanım amacı: Adresi oluşturan kullanıcının adını belirtir
        /// Veritabanında NOT NULL, varsayılan değer: "SYSTEM"
        /// </summary>
        [StringLength(20, ErrorMessage = "Oluşturan kullanıcı adı en fazla 20 karakter olabilir")]
        public string CreatedUserName { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı adı - prCurrAccPostalAddress.LastUpdatedUserName alanı ile eşleşir
        /// Kullanım amacı: Adresi son güncelleyen kullanıcının adını belirtir
        /// Veritabanında NOT NULL, varsayılan değer: "SYSTEM"
        /// </summary>
        [StringLength(20, ErrorMessage = "Son güncelleyen kullanıcı adı en fazla 20 karakter olabilir")]
        public string LastUpdatedUserName { get; set; }
        
        /// <summary>
        /// Varsayılan adres mi? - prCurrAccDefault tablosu ile ilişkilidir
        /// Kullanım amacı: Bu adresin müşterinin varsayılan adresi olup olmadığını belirtir
        /// Varsayılan değer: false
        /// </summary>
        public bool IsDefault { get; set; }
    }
} 