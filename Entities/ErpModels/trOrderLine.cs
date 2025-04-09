using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderLine")]
    public partial class trOrderLine
    {
        public trOrderLine()
        {
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            tpOrderCancelDetails = new HashSet<tpOrderCancelDetail>();
            tpOrderCanceleds = new HashSet<tpOrderCanceled>();
            tpOrderDeliveryDetails = new HashSet<tpOrderDeliveryDetail>();
            tpOrderDiscountOffers = new HashSet<tpOrderDiscountOffer>();
            tpOrderDiscountOfferContributors = new HashSet<tpOrderDiscountOfferContributor>();
            tpOrderITAttributes = new HashSet<tpOrderITAttribute>();
            tpOrderLineExtensions = new HashSet<tpOrderLineExtension>();
            tpOrderLineSerialNumbers = new HashSet<tpOrderLineSerialNumber>();
            tpOrderOpticalProductCustomProcesss = new HashSet<tpOrderOpticalProductCustomProcess>();
            tpOrderOTAttributes = new HashSet<tpOrderOTAttribute>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            trAdjustCostOrderLines = new HashSet<trAdjustCostOrderLine>();
            trAllocationProducts = new HashSet<trAllocationProduct>();
            trDispOrderLines = new HashSet<trDispOrderLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trInvoiceLineSubsequentDeliveryOrderss = new HashSet<trInvoiceLineSubsequentDeliveryOrders>();
            trOrderAsnLines = new HashSet<trOrderAsnLine>();
            trOrderLineBOMs = new HashSet<trOrderLineBOM>();
            trOrderLineCurrencys = new HashSet<trOrderLineCurrency>();
            trOrderOpticalProductLines = new HashSet<trOrderOpticalProductLine>();
            trPickingLines = new HashSet<trPickingLine>();
            trReserveLines = new HashSet<trReserveLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

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

        [Required]
        public double CancelQty1 { get; set; }

        [Required]
        public double CancelQty2 { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string OrderCancelReasonCode { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        public bool? IsClosed { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PurchasePlanCode { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public DateTime PlannedDateOfLading { get; set; }

        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WithHoldingTaxTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DOVCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VatCode { get; set; }

        [Required]
        public float VatRate { get; set; }

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

        public Guid? PriceListLineID { get; set; }

        [Required]
        public object BaseProcessCode { get; set; }

        [Required]
        public object BaseOrderNumber { get; set; }

        [Required]
        public int BaseCustomerTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BaseCustomerCode { get; set; }

        public Guid? BaseSubCurrAccID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BaseStoreCode { get; set; }

        public Guid? SupportRequestHeaderID { get; set; }

        public Guid? PurchaseRequisitionLineID { get; set; }

        public Guid? OrderLineLinkedProductID { get; set; }

        [Required]
        public double SurplusOrderQtyToleranceRate { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

        [Required]
        public int OrderLineSumID { get; set; }

        public int? OrderLineBOMID { get; set; }

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
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }
        public virtual trOrderLineLinkedProduct trOrderLineLinkedProduct { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual cdSalesperson cdSalesperson { get; set; }
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }
        public virtual trPriceListLine trPriceListLine { get; set; }
        public virtual cdDOV cdDOV { get; set; }
        public virtual bsWithHoldingTaxType bsWithHoldingTaxType { get; set; }
        public virtual cdPurchasePlan cdPurchasePlan { get; set; }
        public virtual trOrderHeader trOrderHeader { get; set; }
        public virtual cdVat cdVat { get; set; }
        public virtual cdOrderCancelReason cdOrderCancelReason { get; set; }
        public virtual cdPCT cdPCT { get; set; }
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }

        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<tpOrderCancelDetail> tpOrderCancelDetails { get; set; }
        public virtual ICollection<tpOrderCanceled> tpOrderCanceleds { get; set; }
        public virtual ICollection<tpOrderDeliveryDetail> tpOrderDeliveryDetails { get; set; }
        public virtual ICollection<tpOrderDiscountOffer> tpOrderDiscountOffers { get; set; }
        public virtual ICollection<tpOrderDiscountOfferContributor> tpOrderDiscountOfferContributors { get; set; }
        public virtual ICollection<tpOrderITAttribute> tpOrderITAttributes { get; set; }
        public virtual ICollection<tpOrderLineExtension> tpOrderLineExtensions { get; set; }
        public virtual ICollection<tpOrderLineSerialNumber> tpOrderLineSerialNumbers { get; set; }
        public virtual ICollection<tpOrderOpticalProductCustomProcess> tpOrderOpticalProductCustomProcesss { get; set; }
        public virtual ICollection<tpOrderOTAttribute> tpOrderOTAttributes { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<trAdjustCostOrderLine> trAdjustCostOrderLines { get; set; }
        public virtual ICollection<trAllocationProduct> trAllocationProducts { get; set; }
        public virtual ICollection<trDispOrderLine> trDispOrderLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trInvoiceLineSubsequentDeliveryOrders> trInvoiceLineSubsequentDeliveryOrderss { get; set; }
        public virtual ICollection<trOrderAsnLine> trOrderAsnLines { get; set; }
        public virtual ICollection<trOrderLineBOM> trOrderLineBOMs { get; set; }
        public virtual ICollection<trOrderLineCurrency> trOrderLineCurrencys { get; set; }
        public virtual ICollection<trOrderOpticalProductLine> trOrderOpticalProductLines { get; set; }
        public virtual ICollection<trPickingLine> trPickingLines { get; set; }
        public virtual ICollection<trReserveLine> trReserveLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
    }
}
