using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderHeader")]
    public partial class trOrderHeader
    {
        public trOrderHeader()
        {
            tpDistanceSaleBankPayments = new HashSet<tpDistanceSaleBankPayment>();
            tpInnerLineDocuments = new HashSet<tpInnerLineDocument>();
            tpOrderATAttributes = new HashSet<tpOrderATAttribute>();
            tpOrderCancelDetailHeaders = new HashSet<tpOrderCancelDetailHeader>();
            tpOrderCashRegisterInfos = new HashSet<tpOrderCashRegisterInfo>();
            tpOrderContractContexts = new HashSet<tpOrderContractContext>();
            tpOrderDiscountOffers = new HashSet<tpOrderDiscountOffer>();
            tpOrderDiscountOfferContributors = new HashSet<tpOrderDiscountOfferContributor>();
            tpOrderFTAttributes = new HashSet<tpOrderFTAttribute>();
            tpOrderHeaderExtensions = new HashSet<tpOrderHeaderExtension>();
            tpOrdersViaInternetInfos = new HashSet<tpOrdersViaInternetInfo>();
            trAdjustCostOrders = new HashSet<trAdjustCostOrder>();
            trOrderAuditorSurveys = new HashSet<trOrderAuditorSurvey>();
            trOrderLines = new HashSet<trOrderLine>();
            trOrderLineSums = new HashSet<trOrderLineSum>();
            trOrderLineSumDetails = new HashSet<trOrderLineSumDetail>();
            trOrderOpticalProducts = new HashSet<trOrderOpticalProduct>();
            trOrderPaymentPlans = new HashSet<trOrderPaymentPlan>();
            trOrderSurveys = new HashSet<trOrderSurvey>();
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [Required]
        public byte OrderTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object OrderNumber { get; set; }

        [Required]
        public bool IsCancelOrder { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public TimeSpan OrderTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ToWarehouseCode { get; set; }

        [Required]
        public object OrdererCompanyCode { get; set; }

        [Required]
        public object OrdererOfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string OrdererStoreCode { get; set; }

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
        public double SurplusOrderQtyToleranceRate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode2 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LettersOfCreditNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PaymentMethodCode { get; set; }

        [Required]
        public bool IsInclutedVat { get; set; }

        [Required]
        public bool IsCreditSale { get; set; }

        [Required]
        public bool IsCreditableConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreditableConfirmedUser { get; set; }

        [Required]
        public DateTime CreditableConfirmedDate { get; set; }

        [Required]
        public bool IsSalesViaInternet { get; set; }

        [Required]
        public bool IsProposalBased { get; set; }

        [Required]
        public bool IsSuspended { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool UserLocked { get; set; }

        [Required]
        public bool IsClosed { get; set; }

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
        public virtual cdDiscountReason cdDiscountReason { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual bsIncoterm bsIncoterm { get; set; }
        public virtual bsWithHoldingTaxType bsWithHoldingTaxType { get; set; }
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdDOV cdDOV { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual bsOrderType bsOrderType { get; set; }
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdPaymentMethod cdPaymentMethod { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsTaxType bsTaxType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual bsTaxExemption bsTaxExemption { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpDistanceSaleBankPayment> tpDistanceSaleBankPayments { get; set; }
        public virtual ICollection<tpInnerLineDocument> tpInnerLineDocuments { get; set; }
        public virtual ICollection<tpOrderATAttribute> tpOrderATAttributes { get; set; }
        public virtual ICollection<tpOrderCancelDetailHeader> tpOrderCancelDetailHeaders { get; set; }
        public virtual ICollection<tpOrderCashRegisterInfo> tpOrderCashRegisterInfos { get; set; }
        public virtual ICollection<tpOrderContractContext> tpOrderContractContexts { get; set; }
        public virtual ICollection<tpOrderDiscountOffer> tpOrderDiscountOffers { get; set; }
        public virtual ICollection<tpOrderDiscountOfferContributor> tpOrderDiscountOfferContributors { get; set; }
        public virtual ICollection<tpOrderFTAttribute> tpOrderFTAttributes { get; set; }
        public virtual ICollection<tpOrderHeaderExtension> tpOrderHeaderExtensions { get; set; }
        public virtual ICollection<tpOrdersViaInternetInfo> tpOrdersViaInternetInfos { get; set; }
        public virtual ICollection<trAdjustCostOrder> trAdjustCostOrders { get; set; }
        public virtual ICollection<trOrderAuditorSurvey> trOrderAuditorSurveys { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trOrderLineSum> trOrderLineSums { get; set; }
        public virtual ICollection<trOrderLineSumDetail> trOrderLineSumDetails { get; set; }
        public virtual ICollection<trOrderOpticalProduct> trOrderOpticalProducts { get; set; }
        public virtual ICollection<trOrderPaymentPlan> trOrderPaymentPlans { get; set; }
        public virtual ICollection<trOrderSurvey> trOrderSurveys { get; set; }
    }
}
