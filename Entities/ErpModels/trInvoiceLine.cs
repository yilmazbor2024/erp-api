using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInvoiceLine")]
    public partial class trInvoiceLine
    {
        public trInvoiceLine()
        {
            prFixedAssetExpenses = new HashSet<prFixedAssetExpense>();
            prFixedAssetPurchasess = new HashSet<prFixedAssetPurchases>();
            prFixedAssetSaless = new HashSet<prFixedAssetSales>();
            stItemRollNumbers = new HashSet<stItemRollNumber>();
            stItemSerialNumbers = new HashSet<stItemSerialNumber>();
            tpInnerCustomsTransferImportInvoiceLines = new HashSet<tpInnerCustomsTransferImportInvoiceLine>();
            tpInnerLinePurchaseInvoiceLines = new HashSet<tpInnerLinePurchaseInvoiceLine>();
            tpInvoiceDiscountOffers = new HashSet<tpInvoiceDiscountOffer>();
            tpInvoiceDiscountOfferContributors = new HashSet<tpInvoiceDiscountOfferContributor>();
            tpInvoiceITAttributes = new HashSet<tpInvoiceITAttribute>();
            tpInvoiceLineAgentPerformances = new HashSet<tpInvoiceLineAgentPerformance>();
            tpInvoiceLineExpenseAccruals = new HashSet<tpInvoiceLineExpenseAccrual>();
            tpInvoiceLineExtensions = new HashSet<tpInvoiceLineExtension>();
            tpInvoiceLineOpticalProductInfos = new HashSet<tpInvoiceLineOpticalProductInfo>();
            tpInvoiceLinePickingDetailss = new HashSet<tpInvoiceLinePickingDetails>();
            tpInvoiceOpticalContributions = new HashSet<tpInvoiceOpticalContribution>();
            tpInvoiceUnAcceptableExpenseLines = new HashSet<tpInvoiceUnAcceptableExpenseLine>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            trAdjustCostExpenseInvoiceLines = new HashSet<trAdjustCostExpenseInvoiceLine>();
            trAdjustCostInvoiceLines = new HashSet<trAdjustCostInvoiceLine>();
            trInvoiceLineBOMs = new HashSet<trInvoiceLineBOM>();
            trInvoiceLineCostCenterRatess = new HashSet<trInvoiceLineCostCenterRates>();
            trInvoiceLineCurrencys = new HashSet<trInvoiceLineCurrency>();
            trInvoiceLineGiftCards = new HashSet<trInvoiceLineGiftCard>();
            trInvoiceLineReportedSaless = new HashSet<trInvoiceLineReportedSales>();
            trInvoiceLineSubsequentDeliveryOrderss = new HashSet<trInvoiceLineSubsequentDeliveryOrders>();
            trTaxIncurredLines = new HashSet<trTaxIncurredLine>();
        }

        [Key]
        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SectionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PurchasePlanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ReturnReasonCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [Required]
        public bool IsTransformed { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public DateTime PlannedDateOfLading { get; set; }

        public DateTime? OrderDeliveryDate { get; set; }

        public DateTime? ManufactureDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        public object ImportFileNumber { get; set; }

        [Required]
        public object ExportFileNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VatCode { get; set; }

        [Required]
        public float VatRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WithHoldingTaxTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DOVCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PCTCode { get; set; }

        [Required]
        public float PCTRate { get; set; }

        [Required]
        public double LDisRate1 { get; set; }

        [Required]
        public double LDisRate2 { get; set; }

        [Required]
        public double LDisRate3 { get; set; }

        [Required]
        public double LDisRate4 { get; set; }

        [Required]
        public double LDisRate5 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public double PriceExchangeRate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsImmutable { get; set; }

        public Guid? SupportRequestHeaderID { get; set; }

        public Guid? PurchaseRequisitionLineID { get; set; }

        public Guid? ShipmentLineID { get; set; }

        public Guid? ReserveLineID { get; set; }

        public Guid? DispOrderLineID { get; set; }

        public Guid? PickingLineID { get; set; }

        public Guid? OrderAsnLineID { get; set; }

        public Guid? OrderLineID { get; set; }

        public Guid? PriceListLineID { get; set; }

        public Guid? InvoiceLineLinkedProductID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Required]
        public int InvoiceLineSumID { get; set; }

        [Required]
        public int InvoiceLineSerialSumID { get; set; }

        public int? InvoiceLineBOMID { get; set; }

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
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }
        public virtual trPickingLine trPickingLine { get; set; }
        public virtual cdReturnReason cdReturnReason { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual trInvoiceLineLinkedProduct trInvoiceLineLinkedProduct { get; set; }
        public virtual cdPurchasePlan cdPurchasePlan { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual bsWithHoldingTaxType bsWithHoldingTaxType { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual trOrderAsnLine trOrderAsnLine { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trDispOrderLine trDispOrderLine { get; set; }
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }
        public virtual trPriceListLine trPriceListLine { get; set; }
        public virtual cdDOV cdDOV { get; set; }
        public virtual cdSalesperson cdSalesperson { get; set; }
        public virtual cdPCT cdPCT { get; set; }
        public virtual trReserveLine trReserveLine { get; set; }
        public virtual cdVat cdVat { get; set; }
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }
        public virtual trShipmentLine trShipmentLine { get; set; }

        public virtual ICollection<prFixedAssetExpense> prFixedAssetExpenses { get; set; }
        public virtual ICollection<prFixedAssetPurchases> prFixedAssetPurchasess { get; set; }
        public virtual ICollection<prFixedAssetSales> prFixedAssetSaless { get; set; }
        public virtual ICollection<stItemRollNumber> stItemRollNumbers { get; set; }
        public virtual ICollection<stItemSerialNumber> stItemSerialNumbers { get; set; }
        public virtual ICollection<tpInnerCustomsTransferImportInvoiceLine> tpInnerCustomsTransferImportInvoiceLines { get; set; }
        public virtual ICollection<tpInnerLinePurchaseInvoiceLine> tpInnerLinePurchaseInvoiceLines { get; set; }
        public virtual ICollection<tpInvoiceDiscountOffer> tpInvoiceDiscountOffers { get; set; }
        public virtual ICollection<tpInvoiceDiscountOfferContributor> tpInvoiceDiscountOfferContributors { get; set; }
        public virtual ICollection<tpInvoiceITAttribute> tpInvoiceITAttributes { get; set; }
        public virtual ICollection<tpInvoiceLineAgentPerformance> tpInvoiceLineAgentPerformances { get; set; }
        public virtual ICollection<tpInvoiceLineExpenseAccrual> tpInvoiceLineExpenseAccruals { get; set; }
        public virtual ICollection<tpInvoiceLineExtension> tpInvoiceLineExtensions { get; set; }
        public virtual ICollection<tpInvoiceLineOpticalProductInfo> tpInvoiceLineOpticalProductInfos { get; set; }
        public virtual ICollection<tpInvoiceLinePickingDetails> tpInvoiceLinePickingDetailss { get; set; }
        public virtual ICollection<tpInvoiceOpticalContribution> tpInvoiceOpticalContributions { get; set; }
        public virtual ICollection<tpInvoiceUnAcceptableExpenseLine> tpInvoiceUnAcceptableExpenseLines { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<trAdjustCostExpenseInvoiceLine> trAdjustCostExpenseInvoiceLines { get; set; }
        public virtual ICollection<trAdjustCostInvoiceLine> trAdjustCostInvoiceLines { get; set; }
        public virtual ICollection<trInvoiceLineBOM> trInvoiceLineBOMs { get; set; }
        public virtual ICollection<trInvoiceLineCostCenterRates> trInvoiceLineCostCenterRatess { get; set; }
        public virtual ICollection<trInvoiceLineCurrency> trInvoiceLineCurrencys { get; set; }
        public virtual ICollection<trInvoiceLineGiftCard> trInvoiceLineGiftCards { get; set; }
        public virtual ICollection<trInvoiceLineReportedSales> trInvoiceLineReportedSaless { get; set; }
        public virtual ICollection<trInvoiceLineSubsequentDeliveryOrders> trInvoiceLineSubsequentDeliveryOrderss { get; set; }
        public virtual ICollection<trTaxIncurredLine> trTaxIncurredLines { get; set; }
    }
}
