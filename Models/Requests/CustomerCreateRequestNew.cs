using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace erp_api.Models.Requests
{
    /// <summary>
    /// Yeni müşteri oluşturma isteği modeli
    /// </summary>
    public class CustomerCreateRequestNew
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
        [StringLength(250, ErrorMessage = "Müşteri adı en fazla 250 karakter olabilir")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Müşteri soyadı (Bireysel müşteriler için)
        /// </summary>
        [StringLength(100, ErrorMessage = "Müşteri soyadı en fazla 100 karakter olabilir")]
        public string CustomerSurname { get; set; }

        /// <summary>
        /// Müşteri tipi kodu
        /// </summary>
        [Required(ErrorMessage = "Müşteri tipi zorunludur")]
        public int CustomerTypeCode { get; set; }

        /// <summary>
        /// Bireysel müşteri mi?
        /// </summary>
        public bool IsIndividualAcc { get; set; }

        /// <summary>
        /// Unvan kodu
        /// </summary>
        public string TitleCode { get; set; }

        /// <summary>
        /// Baba adı
        /// </summary>
        public string Patronym { get; set; }

        /// <summary>
        /// Kimlik numarası
        /// </summary>
        public string CustomerIdentityNumber { get; set; }

        /// <summary>
        /// Vergi numarası
        /// </summary>
        [StringLength(20, ErrorMessage = "Vergi numarası en fazla 20 karakter olabilir")]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Müşteri oluşturan kullanıcı adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir")]
        public string CreatedUserName { get; set; } = "SYSTEM";

        /// <summary>
        /// Müşteriyi son güncelleyen kullanıcı adı
        /// </summary>
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir")]
        public string LastUpdatedUserName { get; set; } = "SYSTEM";

        /// <summary>
        /// Mersis numarası
        /// </summary>
        public string MersisNum { get; set; }

        /// <summary>
        /// İndirim grubu kodu
        /// </summary>
        public string DiscountGroupCode { get; set; }

        /// <summary>
        /// Müşteri markup grubu kodu
        /// </summary>
        public string CustomerMarkupGrCode { get; set; }

        /// <summary>
        /// Müşteri ödeme planı grubu kodu
        /// </summary>
        public string CustomerPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Tedarikçi ödeme planı grubu kodu
        /// </summary>
        public string VendorPaymentPlanGrCode { get; set; }

        /// <summary>
        /// Şirket kodu
        /// </summary>
        public string CompanyCode { get; set; } = "1";

        /// <summary>
        /// Para birimi kodu
        /// </summary>
        [Required(ErrorMessage = "Para birimi zorunludur")]
        public string CurrencyCode { get; set; } = "TRY";

        /// <summary>
        /// Ofis kodu
        /// </summary>
        [Required(ErrorMessage = "Ofis kodu zorunludur")]
        public string OfficeCode { get; set; } = "M";

        /// <summary>
        /// Satış temsilcisi kodu
        /// </summary>
        public string SalesmanCode { get; set; }

        /// <summary>
        /// Kredi limiti
        /// </summary>
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// Risk limiti
        /// </summary>
        public decimal RiskLimit { get; set; }

        /// <summary>
        /// Minimum bakiye
        /// </summary>
        public decimal MinBalance { get; set; }

        /// <summary>
        /// Vergi dairesi kodu
        /// </summary>
        public string TaxOfficeCode { get; set; }

        /// <summary>
        /// Bölge kodu
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Şehir kodu
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// İlçe kodu
        /// </summary>
        public string DistrictCode { get; set; }

        /// <summary>
        /// Müşteri bloke mi?
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Müşteri kilitli mi?
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Kilit tarihi
        /// </summary>
        public DateTime? LockedDate { get; set; }

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
        public string DueDateFormulaCode { get; set; }

        /// <summary>
        /// Banka kodu
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// Banka şube kodu
        /// </summary>
        public string BankBranchCode { get; set; }

        /// <summary>
        /// Banka hesap tipi kodu
        /// </summary>
        public int BankAccTypeCode { get; set; }

        /// <summary>
        /// IBAN
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// SWIFT kodu
        /// </summary>
        public string SWIFTCode { get; set; }

        /// <summary>
        /// Banka hesap numarası
        /// </summary>
        public string BankAccNo { get; set; }

        /// <summary>
        /// Tedarikçi tipi kodu
        /// </summary>
        public int VendorTypeCode { get; set; }

        /// <summary>
        /// Perakende satış fiyatı grubu kodu
        /// </summary>
        public string RetailSalePriceGroupCode { get; set; }

        /// <summary>
        /// Toptan satış fiyatı grubu kodu
        /// </summary>
        public string WholesalePriceGroupCode { get; set; }

        /// <summary>
        /// Hesap açılış tarihi
        /// </summary>
        public DateTime? AccountOpeningDate { get; set; }

        /// <summary>
        /// Hesap kapanış tarihi
        /// </summary>
        public DateTime? AccountClosingDate { get; set; }

        /// <summary>
        /// Promosyon grubu kodu
        /// </summary>
        public string PromotionGroupCode { get; set; }

        /// <summary>
        /// Satış kanalı kodu
        /// </summary>
        public string SalesChannelCode { get; set; }

        /// <summary>
        /// Üretim kullanılsın mı?
        /// </summary>
        public bool UseManufacturing { get; set; }

        /// <summary>
        /// Barkod tipi kodu
        /// </summary>
        public string BarcodeTypeCode { get; set; } = "Def";

        /// <summary>
        /// Maliyet merkezi kodu
        /// </summary>
        public string CostCenterCode { get; set; }

        /// <summary>
        /// Mağazada banka hesabı kullanılsın mı?
        /// </summary>
        public bool UseBankAccOnStore { get; set; }

        /// <summary>
        /// Genel muhasebe tipi kodu
        /// </summary>
        public string GLTypeCode { get; set; }

        /// <summary>
        /// E-faturaya tabi mi?
        /// </summary>
        public bool IsSubjectToEInvoice { get; set; }

        /// <summary>
        /// Ticari fatura düzenlensin mi?
        /// </summary>
        public bool IsArrangeCommercialInvoice { get; set; }

        /// <summary>
        /// Cari hesap lot grubu kodu
        /// </summary>
        public string CurrAccLotGrCode { get; set; }

        /// <summary>
        /// Sadece seçili para birimi izin verilsin mi?
        /// </summary>
        public bool AllowOnlySelectedCurrency { get; set; }

        /// <summary>
        /// Kredi bakiyesine izin verilsin mi?
        /// </summary>
        public bool PermitCreditBalance { get; set; }

        /// <summary>
        /// E-irsaliyeye tabi mi?
        /// </summary>
        public bool IsSubjectToEShipment { get; set; }

        /// <summary>
        /// Müşteri ASN numarası sevkiyatlar için gerekli mi?
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
        public string DBSAccountCode { get; set; }

        /// <summary>
        /// Seri numarası takibi kullanılsın mı?
        /// </summary>
        public bool UseSerialNumberTracking { get; set; }

        /// <summary>
        /// E-fatura başlangıç tarihi
        /// </summary>
        public DateTime? EInvoiceStartDate { get; set; }

        /// <summary>
        /// E-irsaliye başlangıç tarihi
        /// </summary>
        public DateTime? EShipmentStartDate { get; set; }

        /// <summary>
        /// Veri dili kodu
        /// </summary>
        public string DataLanguageCode { get; set; } = "TR";

        /// <summary>
        /// Mağaza hiyerarşi ID
        /// </summary>
        public int? StoreHierarchyID { get; set; }

        /// <summary>
        /// Anlaşma tarihi
        /// </summary>
        public DateTime? AgreementDate { get; set; }

        /// <summary>
        /// Ödeme vadesi
        /// </summary>
        public int? PaymentTerm { get; set; }

        /// <summary>
        /// Müşteri adresleri
        /// </summary>
        public List<CustomerAddressCreateRequestNew> Addresses { get; set; }

        /// <summary>
        /// Müşteri iletişim bilgileri
        /// </summary>
        public List<CustomerCommunicationCreateRequestNew> Communications { get; set; }

        /// <summary>
        /// Müşteri iletişim kişileri
        /// </summary>
        public List<CustomerContactCreateRequestNew> Contacts { get; set; }
    }
}
