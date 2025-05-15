using System;

namespace ErpMobile.Api.Models.Responses
{
    /// <summary>
    /// Müşteri güncelleme yanıt modeli
    /// </summary>
    public class CustomerUpdateResponseNew
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// İşlem mesajı
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Hata detayları
        /// </summary>
        public string ErrorDetails { get; set; }

        /// <summary>
        /// Müşteri kodu
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Müşteri tipi kodu
        /// </summary>
        public int CustomerTypeCode { get; set; }

        /// <summary>
        /// Müşteri tipi açıklaması
        /// </summary>
        public string CustomerTypeDescription { get; set; }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        public string CreatedUsername { get; set; }

        /// <summary>
        /// Para birimi kodu
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// VIP müşteri mi?
        /// </summary>
        public bool IsVIP { get; set; }

        /// <summary>
        /// Şirket kodu
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Ofis kodu
        /// </summary>
        public string OfficeCode { get; set; }

        /// <summary>
        /// Ofis açıklaması
        /// </summary>
        public string OfficeDescription { get; set; }

        /// <summary>
        /// Kimlik numarası
        /// </summary>
        public string IdentityNum { get; set; }

        /// <summary>
        /// Vergi numarası
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Vergi dairesi kodu
        /// </summary>
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// E-Fatura'ya tabi mi?
        /// </summary>
        public bool IsSubjectToEInvoice { get; set; }

        /// <summary>
        /// DBS entegrasyonu kullanılsın mı?
        /// </summary>
        public bool UseDBSIntegration { get; set; }

        /// <summary>
        /// Müşteri bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Varsayılan posta adresi ID'si
        /// </summary>
        public Guid? DefaultPostalAddressID { get; set; }

        /// <summary>
        /// Varsayılan iletişim bilgisi ID'si
        /// </summary>
        public Guid? DefaultCommunicationID { get; set; }

        /// <summary>
        /// Varsayılan kişi ID'si
        /// </summary>
        public Guid? DefaultContactID { get; set; }

        /// <summary>
        /// Adres sayısı
        /// </summary>
        public int AddressCount { get; set; }

        /// <summary>
        /// İletişim bilgisi sayısı
        /// </summary>
        public int CommunicationCount { get; set; }

        /// <summary>
        /// Kişi sayısı
        /// </summary>
        public int ContactCount { get; set; }
    }
}
