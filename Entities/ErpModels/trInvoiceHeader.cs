using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInvoiceHeader")]
    public partial class trInvoiceHeader
    {
        public trInvoiceHeader()
        {
            prCompanyCreditCardEarnedPointss = new HashSet<prCompanyCreditCardEarnedPoints>();
            tpAgentPerformanceBonusDebits = new HashSet<tpAgentPerformanceBonusDebit>();
            tpCashRegisterInfos = new HashSet<tpCashRegisterInfo>();
            tpEArchieveIntegratorInfos = new HashSet<tpEArchieveIntegratorInfo>();
            tpEArchiveInvoiceConfirmations = new HashSet<tpEArchiveInvoiceConfirmation>();
            tpExpenseInvoiceConfirmations = new HashSet<tpExpenseInvoiceConfirmation>();
            tpExportSaleRealisitions = new HashSet<tpExportSaleRealisition>();
            tpInnerLineDocuments = new HashSet<tpInnerLineDocument>();
            tpInvoiceadditionalDeliveryProcessesDistances = new HashSet<tpInvoiceadditionalDeliveryProcessesDistance>();
            tpInvoiceATAttributes = new HashSet<tpInvoiceATAttribute>();
            tpInvoiceCancelDBSBankIntegrations = new HashSet<tpInvoiceCancelDBSBankIntegration>();
            tpInvoiceDiscountOffers = new HashSet<tpInvoiceDiscountOffer>();
            tpInvoiceDiscountOfferContributors = new HashSet<tpInvoiceDiscountOfferContributor>();
            tpInvoiceEArchieveXMLs = new HashSet<tpInvoiceEArchieveXML>();
            tpInvoiceEInvoiceXMLs = new HashSet<tpInvoiceEInvoiceXML>();
            tpInvoiceExchangeDifferencePaidCheques = new HashSet<tpInvoiceExchangeDifferencePaidCheque>();
            tpInvoiceFTAttributes = new HashSet<tpInvoiceFTAttribute>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
            tpInvoiceHeaderSalesPersons = new HashSet<tpInvoiceHeaderSalesPerson>();
            tpInvoiceLinePickingDetailss = new HashSet<tpInvoiceLinePickingDetails>();
            tpInvoiceOpticalContributions = new HashSet<tpInvoiceOpticalContribution>();
            tpInvoicePassportAndBoardingInfos = new HashSet<tpInvoicePassportAndBoardingInfo>();
            tpInvoiceSGKExtensionss = new HashSet<tpInvoiceSGKExtensions>();
            tpInvoiceSourceInfos = new HashSet<tpInvoiceSourceInfo>();
            tpInvoiceTransportModeDetails = new HashSet<tpInvoiceTransportModeDetail>();
            tpInvoiceUBLExtensionss = new HashSet<tpInvoiceUBLExtensions>();
            tpJournalTaxIncurreds = new HashSet<tpJournalTaxIncurred>();
            tpJournalZNumDetails = new HashSet<tpJournalZNumDetail>();
            tpOrderCancelDetailHeaders = new HashSet<tpOrderCancelDetailHeader>();
            tpSalesViaInternetInfos = new HashSet<tpSalesViaInternetInfo>();
            trAdjustCostInvoices = new HashSet<trAdjustCostInvoice>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trInvoiceLineSums = new HashSet<trInvoiceLineSum>();
            trInvoiceLineSumDetails = new HashSet<trInvoiceLineSumDetail>();
            trTFRSInvoiceAdjustments = new HashSet<trTFRSInvoiceAdjustment>();
            zpJoyRefundTransactions = new HashSet<zpJoyRefundTransaction>();
            zpTaxFreeZoneTransactions = new HashSet<zpTaxFreeZoneTransaction>();
            zpWeArePlanetTaxFreeTransactions = new HashSet<zpWeArePlanetTaxFreeTransaction>();
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object InvoiceNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public TimeSpan InvoiceTime { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Series { get; set; }

        [Required]
        public decimal SeriesNumber { get; set; }

        [Required]
        public byte InvoiceTypeCode { get; set; }

        [Required]
        public byte ExpenseTypeCode { get; set; }

        [Required]
        public bool IsEInvoice { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EInvoiceNumber { get; set; }

        [Required]
        public byte EInvoiceStatusCode { get; set; }

        [Required]
        public short PaymentTerm { get; set; }

        [Required]
        public DateTime AverageDueDate { get; set; }

        public string Description { get; set; }

        public string InternalDescription { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EInvoiceAliasCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyCreditCardCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        public Guid? ShippingPostalAddressID { get; set; }

        public Guid? BillingPostalAddressID { get; set; }

        public Guid? GuarantorContactID { get; set; }

        public Guid? GuarantorContactID2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeliveryCompanyBarcode { get; set; }

        [Required]
        public byte TaxTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WithHoldingTaxTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DOVCode { get; set; }

        [Required]
        public short TaxExemptionCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }
 

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public byte FormType { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DigitalMarketingServiceCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public double TDisRate1 { get; set; }

        [Required]
        public double TDisRate2 { get; set; }

        [Required]
        public double TDisRate3 { get; set; }

        [Required]
        public double TDisRate4 { get; set; }

        [Required]
        public double TDisRate5 { get; set; }

        [Required]
        public byte DiscountReasonCode { get; set; }

        [Required]
        public double StoppageRate { get; set; }

        [Required]
        public decimal TaxRefund { get; set; }

        [Required]
        public object ImportFileNumber { get; set; }

        [Required]
        public object ExportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomsDocumentNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PaymentMethodCode { get; set; }

        [Required]
        public byte DocumentTypeCode { get; set; }

        [Required]
        public bool IsInclutedVat { get; set; }

        [Required]
        public bool IsCreditSale { get; set; }

        [Required]
        public bool IsShipmentBase { get; set; }

        [Required]
        public bool IsReportedSaleBase { get; set; }

        [Required]
        public bool IsOrderBase { get; set; }

        [Required]
        public bool IsSuspended { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsProforma { get; set; }

        [Required]
        public bool IsDelivered { get; set; }

        [Required]
        public byte FiscalPrintedState { get; set; }

        [Required]
        public bool IsSalesViaInternet { get; set; }

        [Required]
        public bool IsProposalBased { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public bool SendInvoiceByEMail { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EMailAddress { get; set; }

        [Required]
        public bool SendInvoiceBySMS { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string GSMNo { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual bsTransType bsTransType { get; set; }
        public virtual cdExpenseType cdExpenseType { get; set; }
        public virtual cdDOV cdDOV { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdDiscountReason cdDiscountReason { get; set; }
        public virtual bsIncoterm bsIncoterm { get; set; }
        public virtual bsWithHoldingTaxType bsWithHoldingTaxType { get; set; }
        public virtual bsEInvoiceStatus bsEInvoiceStatus { get; set; }
        public virtual cdPaymentMethod cdPaymentMethod { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual bsDocumentType bsDocumentType { get; set; }
        public virtual cdCompanyCreditCard cdCompanyCreditCard { get; set; }
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual bsInvoiceType bsInvoiceType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdDigitalMarketingService cdDigitalMarketingService { get; set; }
        public virtual bsTaxType bsTaxType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual bsTaxExemption bsTaxExemption { get; set; }

        public virtual ICollection<prCompanyCreditCardEarnedPoints> prCompanyCreditCardEarnedPointss { get; set; }
        public virtual ICollection<tpAgentPerformanceBonusDebit> tpAgentPerformanceBonusDebits { get; set; }
        public virtual ICollection<tpCashRegisterInfo> tpCashRegisterInfos { get; set; }
        public virtual ICollection<tpEArchieveIntegratorInfo> tpEArchieveIntegratorInfos { get; set; }
        public virtual ICollection<tpEArchiveInvoiceConfirmation> tpEArchiveInvoiceConfirmations { get; set; }
        public virtual ICollection<tpExpenseInvoiceConfirmation> tpExpenseInvoiceConfirmations { get; set; }
        public virtual ICollection<tpExportSaleRealisition> tpExportSaleRealisitions { get; set; }
        public virtual ICollection<tpInnerLineDocument> tpInnerLineDocuments { get; set; }
        public virtual ICollection<tpInvoiceadditionalDeliveryProcessesDistance> tpInvoiceadditionalDeliveryProcessesDistances { get; set; }
        public virtual ICollection<tpInvoiceATAttribute> tpInvoiceATAttributes { get; set; }
        public virtual ICollection<tpInvoiceCancelDBSBankIntegration> tpInvoiceCancelDBSBankIntegrations { get; set; }
        public virtual ICollection<tpInvoiceDiscountOffer> tpInvoiceDiscountOffers { get; set; }
        public virtual ICollection<tpInvoiceDiscountOfferContributor> tpInvoiceDiscountOfferContributors { get; set; }
        public virtual ICollection<tpInvoiceEArchieveXML> tpInvoiceEArchieveXMLs { get; set; }
        public virtual ICollection<tpInvoiceEInvoiceXML> tpInvoiceEInvoiceXMLs { get; set; }
        public virtual ICollection<tpInvoiceExchangeDifferencePaidCheque> tpInvoiceExchangeDifferencePaidCheques { get; set; }
        public virtual ICollection<tpInvoiceFTAttribute> tpInvoiceFTAttributes { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
        public virtual ICollection<tpInvoiceHeaderSalesPerson> tpInvoiceHeaderSalesPersons { get; set; }
        public virtual ICollection<tpInvoiceLinePickingDetails> tpInvoiceLinePickingDetailss { get; set; }
        public virtual ICollection<tpInvoiceOpticalContribution> tpInvoiceOpticalContributions { get; set; }
        public virtual ICollection<tpInvoicePassportAndBoardingInfo> tpInvoicePassportAndBoardingInfos { get; set; }
        public virtual ICollection<tpInvoiceSGKExtensions> tpInvoiceSGKExtensionss { get; set; }
        public virtual ICollection<tpInvoiceSourceInfo> tpInvoiceSourceInfos { get; set; }
        public virtual ICollection<tpInvoiceTransportModeDetail> tpInvoiceTransportModeDetails { get; set; }
        public virtual ICollection<tpInvoiceUBLExtensions> tpInvoiceUBLExtensionss { get; set; }
        public virtual ICollection<tpJournalTaxIncurred> tpJournalTaxIncurreds { get; set; }
        public virtual ICollection<tpJournalZNumDetail> tpJournalZNumDetails { get; set; }
        public virtual ICollection<tpOrderCancelDetailHeader> tpOrderCancelDetailHeaders { get; set; }
        public virtual ICollection<tpSalesViaInternetInfo> tpSalesViaInternetInfos { get; set; }
        public virtual ICollection<trAdjustCostInvoice> trAdjustCostInvoices { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trInvoiceLineSum> trInvoiceLineSums { get; set; }
        public virtual ICollection<trInvoiceLineSumDetail> trInvoiceLineSumDetails { get; set; }
        public virtual ICollection<trTFRSInvoiceAdjustment> trTFRSInvoiceAdjustments { get; set; }
        public virtual ICollection<zpJoyRefundTransaction> zpJoyRefundTransactions { get; set; }
        public virtual ICollection<zpTaxFreeZoneTransaction> zpTaxFreeZoneTransactions { get; set; }
        public virtual ICollection<zpWeArePlanetTaxFreeTransaction> zpWeArePlanetTaxFreeTransactions { get; set; }
    }
}
