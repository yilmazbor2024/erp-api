using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Requests
{
    /// <summary>
    /// Müşteri güncelleme isteği modeli
    /// </summary>
    public class CustomerUpdateRequestNew
    {
        /// <summary>
        /// Müşteri kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri kodu zorunludur")]
        [StringLength(30, ErrorMessage = "Müşteri kodu en fazla 30 karakter olabilir")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        [Required(ErrorMessage = "Müşteri adı zorunludur")]
        [StringLength(100, ErrorMessage = "Müşteri adı en fazla 100 karakter olabilir")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Müşteri soyadı
        /// </summary>
        [StringLength(100, ErrorMessage = "Müşteri soyadı en fazla 100 karakter olabilir")]
        public string CustomerSurname { get; set; }

        /// <summary>
        /// Müşteri tipi kodu
        /// </summary>
        public int CustomerTypeCode { get; set; }

        /// <summary>
        /// Unvan kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Unvan kodu en fazla 10 karakter olabilir")]
        public string TitleCode { get; set; }

        /// <summary>
        /// Baba adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Baba adı en fazla 50 karakter olabilir")]
        public string Patronym { get; set; }

        /// <summary>
        /// Müşteri kimlik numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Müşteri kimlik numarası en fazla 20 karakter olabilir")]
        public string CustomerIdentityNumber { get; set; }

        /// <summary>
        /// Vergi numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Vergi numarası en fazla 20 karakter olabilir")]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Mersis numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Mersis numarası en fazla 20 karakter olabilir")]
        public string MersisNum { get; set; }

        /// <summary>
        /// Bireysel hesap mı?
        /// </summary>
        public bool IsIndividualAcc { get; set; }

        /// <summary>
        /// Veri dili kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Veri dili kodu en fazla 10 karakter olabilir")]
        public string DataLanguageCode { get; set; }

        /// <summary>
        /// Kredi limiti
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Para birimi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Para birimi kodu en fazla 10 karakter olabilir")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Şirket kodu
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Ofis kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Ofis kodu en fazla 10 karakter olabilir")]
        public string OfficeCode { get; set; }

        /// <summary>
        /// Bölge kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Bölge kodu en fazla 10 karakter olabilir")]
        public string RegionCode { get; set; }

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
        /// Satış temsilcisi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Satış temsilcisi kodu en fazla 10 karakter olabilir")]
        public string SalesmanCode { get; set; }

        /// <summary>
        /// Vergi dairesi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Vergi dairesi kodu en fazla 10 karakter olabilir")]
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// VIP müşteri mi?
        /// </summary>
        public bool IsVIP { get; set; }

        /// <summary>
        /// SMS reklamı gönderilsin mi?
        /// </summary>
        public bool IsSendAdvertSMS { get; set; }

        /// <summary>
        /// E-posta reklamı gönderilsin mi?
        /// </summary>
        public bool IsSendAdvertMail { get; set; }

        /// <summary>
        /// Döviz tipi kodu
        /// </summary>
        public int ExchangeTypeCode { get; set; }

        /// <summary>
        /// Vade formülü kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Vade formülü kodu en fazla 10 karakter olabilir")]
        public string DueDateFormulaCode { get; set; }

        /// <summary>
        /// Banka kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Banka kodu en fazla 10 karakter olabilir")]
        public string BankCode { get; set; }

        /// <summary>
        /// Banka şube kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Banka şube kodu en fazla 10 karakter olabilir")]
        public string BankBranchCode { get; set; }

        /// <summary>
        /// Banka hesap tipi kodu
        /// </summary>
        public int BankAccTypeCode { get; set; }

        /// <summary>
        /// IBAN
        /// </summary>
        [StringLength(50, ErrorMessage = "IBAN en fazla 50 karakter olabilir")]
        public string IBAN { get; set; }

        /// <summary>
        /// SWIFT kodu
        /// </summary>
        [StringLength(20, ErrorMessage = "SWIFT kodu en fazla 20 karakter olabilir")]
        public string SWIFTCode { get; set; }

        /// <summary>
        /// Banka hesap numarası
        /// </summary>
        [StringLength(50, ErrorMessage = "Banka hesap numarası en fazla 50 karakter olabilir")]
        public string BankAccNo { get; set; }

        /// <summary>
        /// Minimum bakiye
        /// </summary>
        public decimal MinBalance { get; set; }

        /// <summary>
        /// Müşteri bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// İndirim grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "İndirim grubu kodu en fazla 10 karakter olabilir")]
        public string DiscountGroupCode { get; set; }

        /// <summary>
        /// Satıcı tipi kodu
        /// </summary>
        public int VendorTypeCode { get; set; }

        /// <summary>
        /// Perakende satış fiyat grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Perakende satış fiyat grubu kodu en fazla 10 karakter olabilir")]
        public string RetailSalePriceGroupCode { get; set; }

        /// <summary>
        /// Toptan satış fiyat grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Toptan satış fiyat grubu kodu en fazla 10 karakter olabilir")]
        public string WholesalePriceGroupCode { get; set; }

        /// <summary>
        /// Hesap açılış tarihi
        /// </summary>
        public DateTime AccountOpeningDate { get; set; }

        /// <summary>
        /// Hesap kapanış tarihi
        /// </summary>
        public DateTime AccountClosingDate { get; set; }

        /// <summary>
        /// Promosyon grubu kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Promosyon grubu kodu en fazla 10 karakter olabilir")]
        public string PromotionGroupCode { get; set; }

        /// <summary>
        /// Satış kanalı kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Satış kanalı kodu en fazla 10 karakter olabilir")]
        public string SalesChannelCode { get; set; }

        /// <summary>
        /// Üretim kullanılsın mı?
        /// </summary>
        public bool UseManufacturing { get; set; }

        /// <summary>
        /// Kilitli mi?
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Kilitleme tarihi
        /// </summary>
        public DateTime LockedDate { get; set; }

        /// <summary>
        /// Barkod tipi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Barkod tipi kodu en fazla 10 karakter olabilir")]
        public string BarcodeTypeCode { get; set; }

        /// <summary>
        /// Maliyet merkezi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Maliyet merkezi kodu en fazla 10 karakter olabilir")]
        public string CostCenterCode { get; set; }

        /// <summary>
        /// Mağazada banka hesabı kullanılsın mı?
        /// </summary>
        public bool UseBankAccOnStore { get; set; }

        /// <summary>
        /// Müşteri ödeme planı grup kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Müşteri ödeme planı grup kodu en fazla 10 karakter olabilir")]
        public string CustomerPaymentPlanGrCode { get; set; }

        /// <summary>
        /// GL tipi kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "GL tipi kodu en fazla 10 karakter olabilir")]
        public string GLTypeCode { get; set; }

        /// <summary>
        /// E-Fatura'ya tabi mi?
        /// </summary>
        public bool IsSubjectToEInvoice { get; set; }

        /// <summary>
        /// Ticari fatura düzenlensin mi?
        /// </summary>
        public bool IsArrangeCommercialInvoice { get; set; }

        /// <summary>
        /// Müşteri markup grup kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Müşteri markup grup kodu en fazla 10 karakter olabilir")]
        public string CustomerMarkupGrCode { get; set; }

        /// <summary>
        /// Cari hesap lot grup kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Cari hesap lot grup kodu en fazla 10 karakter olabilir")]
        public string CurrAccLotGrCode { get; set; }

        /// <summary>
        /// Sadece seçili para birimi kullanılsın mı?
        /// </summary>
        public bool AllowOnlySelectedCurrency { get; set; }

        /// <summary>
        /// Kredi bakiyesine izin verilsin mi?
        /// </summary>
        public bool PermitCreditBalance { get; set; }

        /// <summary>
        /// E-İrsaliye'ye tabi mi?
        /// </summary>
        public bool IsSubjectToEShipment { get; set; }

        /// <summary>
        /// Sevkiyatlar için müşteri ASN numarası gerekli mi?
        /// </summary>
        public bool CustomerASNNumberIsRequiredForShipments { get; set; }

        /// <summary>
        /// Satın alma talebi gerekli mi?
        /// </summary>
        public bool PurchaseRequisitionRequired { get; set; }

        /// <summary>
        /// DBS entegrasyonu kullanılsın mı?
        /// </summary>
        public bool UseDBSIntegration { get; set; }

        /// <summary>
        /// DBS hesap kodu
        /// </summary>
        [StringLength(50, ErrorMessage = "DBS hesap kodu en fazla 50 karakter olabilir")]
        public string DBSAccountCode { get; set; }

        /// <summary>
        /// Seri numarası takibi kullanılsın mı?
        /// </summary>
        public bool UseSerialNumberTracking { get; set; }

        /// <summary>
        /// E-Fatura başlangıç tarihi
        /// </summary>
        public DateTime EInvoiceStartDate { get; set; }

        /// <summary>
        /// E-İrsaliye başlangıç tarihi
        /// </summary>
        public DateTime EShipmentStartDate { get; set; }

        /// <summary>
        /// Satıcı ödeme planı grup kodu
        /// </summary>
        [StringLength(10, ErrorMessage = "Satıcı ödeme planı grup kodu en fazla 10 karakter olabilir")]
        public string VendorPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Oluşturan kullanıcı adı
        /// </summary>
        [StringLength(20, ErrorMessage = "Oluşturan kullanıcı adı en fazla 20 karakter olabilir")]
        public string CreatedUserName { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı adı
        /// </summary>
        [StringLength(20, ErrorMessage = "Son güncelleyen kullanıcı adı en fazla 20 karakter olabilir")]
        public string LastUpdatedUserName { get; set; }

        /// <summary>
        /// Güncellenecek adresler
        /// </summary>
        public List<CustomerAddressUpdateRequestNew> Addresses { get; set; }

        /// <summary>
        /// Güncellenecek iletişim bilgileri
        /// </summary>
        public List<CustomerCommunicationUpdateRequestNew> Communications { get; set; }

        /// <summary>
        /// Güncellenecek kişiler
        /// </summary>
        public List<CustomerContactUpdateRequestNew> Contacts { get; set; }
    }
}
