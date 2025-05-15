using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Yeni müşteri oluşturma isteği modeli
    /// </summary>
    public class CustomerCreateRequestNew
    {
        /// <summary>
        /// Müşteri kodu (otomatik oluşturulabilir)
        /// </summary>
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>
        /// Şirket kodu
        /// </summary>
        [Required(ErrorMessage = "Şirket kodu zorunludur")]
        public short CompanyCode { get; set; } = 1;

        /// <summary>
        /// Ofis kodu
        /// </summary>
        [Required(ErrorMessage = "Ofis kodu zorunludur")]
        [StringLength(5, ErrorMessage = "Ofis kodu en fazla 5 karakter olabilir")]
        public string OfficeCode { get; set; } = "M";

        /// <summary>
        /// Müşteri adı
        /// </summary>
        [Required(ErrorMessage = "Müşteri adı zorunludur")]
        [StringLength(120, ErrorMessage = "Müşteri adı en fazla 120 karakter olabilir")]
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Müşteri soyadı (Bireysel müşteriler için)
        /// </summary>
        [StringLength(100, ErrorMessage = "Müşteri soyadı en fazla 100 karakter olabilir")]
        public string CustomerSurname { get; set; } = string.Empty;

        /// <summary>
        /// Müşteri tipi kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri tipi kodu zorunludur")]
        public byte CustomerTypeCode { get; set; } = 1;

        /// <summary>
        /// Bireysel müşteri mi?
        /// </summary>
        public bool IsIndividualAcc { get; set; } = false;

        /// <summary>
        /// Vergi numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Vergi numarası en fazla 20 karakter olabilir")]
        public string? TaxNumber { get; set; } = string.Empty;

        /// <summary>
        /// Vergi dairesi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Vergi dairesi kodu en fazla 10 karakter olabilir")]
        public string? TaxOfficeCode { get; set; } = string.Empty;

        /// <summary>
        /// TC Kimlik numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "TC Kimlik numarası en fazla 20 karakter olabilir")]
        public string IdentityNum { get; set; } = string.Empty;

        /// <summary>
        /// Müşteri kimlik numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Müşteri kimlik numarası en fazla 20 karakter olabilir")]
        public string CustomerIdentityNumber { get; set; } = string.Empty;

        /// <summary>
        /// Mersis numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Mersis numarası en fazla 20 karakter olabilir")]
        public string? MersisNum { get; set; } = string.Empty;

        /// <summary>
        /// Unvan kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Unvan kodu en fazla 10 karakter olabilir")]
        public string? TitleCode { get; set; } = string.Empty;

        /// <summary>
        /// Baba adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Baba adı en fazla 50 karakter olabilir")]
        public string? Patronym { get; set; } = string.Empty;

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
        /// Bölge kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Bölge kodu en fazla 10 karakter olabilir")]
        public string RegionCode { get; set; } = string.Empty;

        /// <summary>
        /// Ödeme vadesi (gün)
        /// </summary>
        public int PaymentTerm { get; set; } = 0;

        /// <summary>
        /// Vade tarihi formülü kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Vade tarihi formülü kodu en fazla 10 karakter olabilir")]
        public string? DueDateFormulaCode { get; set; } = string.Empty;

        /// <summary>
        /// Para birimi kodu
        /// </summary>
        [StringLength(5, ErrorMessage = "Para birimi kodu en fazla 5 karakter olabilir")]
        public string CurrencyCode { get; set; } = "TRY";

        /// <summary>
        /// Kredi limiti
        /// </summary>
        public decimal CreditLimit { get; set; } = 0;

        /// <summary>
        /// Risk limiti
        /// </summary>
        public decimal RiskLimit { get; set; } = 0;

        /// <summary>
        /// Minimum bakiye
        /// </summary>
        public decimal MinBalance { get; set; } = 0;

        /// <summary>
        /// Veri dili kodu
        /// </summary>
        [Required(ErrorMessage = "Veri dili kodu zorunludur")]
        [StringLength(5, ErrorMessage = "Veri dili kodu en fazla 5 karakter olabilir")]
        public string DataLanguageCode { get; set; } = "TR";

        /// <summary>
        /// Barkod tipi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Barkod tipi kodu en fazla 10 karakter olabilir")]
        public string BarcodeTypeCode { get; set; } = "Def";

        /// <summary>
        /// VIP müşteri mi?
        /// </summary>
        public bool IsVIP { get; set; } = false;

        /// <summary>
        /// SMS reklam gönderimi yapılabilir mi?
        /// </summary>
        public bool IsSendAdvertSMS { get; set; } = false;

        /// <summary>
        /// E-posta reklam gönderimi yapılabilir mi?
        /// </summary>
        public bool IsSendAdvertMail { get; set; } = false;

        /// <summary>
        /// Bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; } = false;

        /// <summary>
        /// Kilitli mi?
        /// </summary>
        public bool IsLocked { get; set; } = false;

        /// <summary>
        /// DBS hesap kodu
        /// </summary>
        [StringLength(20, ErrorMessage = "DBS hesap kodu en fazla 20 karakter olabilir")]
        public string DBSAccountCode { get; set; } = "DEFAULT";

        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        [Required(ErrorMessage = "Oluşturan kullanıcı adı zorunludur")]
        [StringLength(50, ErrorMessage = "Oluşturan kullanıcı adı en fazla 50 karakter olabilir")]
        public string CreatedUserName { get; set; } = "SYSTEM";

        /// <summary>
        /// Son güncelleyen kullanıcı adı
        /// </summary>
        [Required(ErrorMessage = "Son güncelleyen kullanıcı adı zorunludur")]
        [StringLength(50, ErrorMessage = "Son güncelleyen kullanıcı adı en fazla 50 karakter olabilir")]
        public string LastUpdatedUserName { get; set; } = "SYSTEM";

        /// <summary>
        /// Müşteri adresleri
        /// </summary>
        public List<CustomerAddressCreateRequestNew> Addresses { get; set; } = new List<CustomerAddressCreateRequestNew>();

        /// <summary>
        /// Müşteri iletişim bilgileri
        /// </summary>
        public List<CustomerCommunicationCreateRequestNew> Communications { get; set; } = new List<CustomerCommunicationCreateRequestNew>();

        /// <summary>
        /// Müşteri kişileri
        /// </summary>
        public List<CustomerContactCreateRequestNew> Contacts { get; set; } = new List<CustomerContactCreateRequestNew>();

        /// <summary>
        /// DBS entegrasyonu kullanılsın mı?
        /// </summary>
        public bool UseDBSIntegration { get; set; } = false;

        /// <summary>
        /// Gerçek kişi mi?
        /// </summary>
        public bool IsRealPerson { get; set; } = false;

        /// <summary>
        /// Promosyon grup kodu
        /// </summary>
        public string PromotionGroupCode { get; set; } = "";

        /// <summary>
        /// E-Fatura mükellefi mi?
        /// </summary>
        public bool IsSubjectToEInvoice { get; set; } = false;
    }
}
