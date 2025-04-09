using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trProposalLine")]
    public partial class trProposalLine
    {
        public trProposalLine()
        {
            tpProposalDiscountOffers = new HashSet<tpProposalDiscountOffer>();
            tpProposalDiscountOfferContributors = new HashSet<tpProposalDiscountOfferContributor>();
            tpProposalITAttributes = new HashSet<tpProposalITAttribute>();
            tpProposalLineConfirmations = new HashSet<tpProposalLineConfirmation>();
            tpProposalLineConfirmationStatuss = new HashSet<tpProposalLineConfirmationStatus>();
            tpProposalLineRevisions = new HashSet<tpProposalLineRevision>();
            trProposalLineCurrencys = new HashSet<trProposalLineCurrency>();
            trPurchaseRequisitionProposalConfirmationEMailNotificationDetails = new HashSet<trPurchaseRequisitionProposalConfirmationEMailNotificationDetail>();
        }

        [Key]
        [Required]
        public Guid ProposalLineID { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SalespersonCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PurchasePlanCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public DateTime PlannedDateOfLading { get; set; }

        [Required]
        public object ImportFileNumber { get; set; }

        [Required]
        public object ExportFileNumber { get; set; }

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

        [Required]
        public bool ClosedBySystem { get; set; }

        public Guid? PriceListLineID { get; set; }

        [Required]
        public Guid ProposalHeaderID { get; set; }

        [Required]
        public int ProposalLineSumID { get; set; }

        [Required]
        public bool IsTransformed { get; set; }

        public Guid? PurchaseRequisitionLineID { get; set; }

        public Guid? InvoiceLineID { get; set; }

        public Guid? OrderLineID { get; set; }

        public Guid? ClosedByProposalLineID { get; set; }

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
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual trProposalHeader trProposalHeader { get; set; }
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }
        public virtual cdDOV cdDOV { get; set; }
        public virtual trPriceListLine trPriceListLine { get; set; }
        public virtual cdSalesperson cdSalesperson { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual bsWithHoldingTaxType bsWithHoldingTaxType { get; set; }
        public virtual cdPurchasePlan cdPurchasePlan { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdPCT cdPCT { get; set; }
        public virtual cdVat cdVat { get; set; }
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }

        public virtual ICollection<tpProposalDiscountOffer> tpProposalDiscountOffers { get; set; }
        public virtual ICollection<tpProposalDiscountOfferContributor> tpProposalDiscountOfferContributors { get; set; }
        public virtual ICollection<tpProposalITAttribute> tpProposalITAttributes { get; set; }
        public virtual ICollection<tpProposalLineConfirmation> tpProposalLineConfirmations { get; set; }
        public virtual ICollection<tpProposalLineConfirmationStatus> tpProposalLineConfirmationStatuss { get; set; }
        public virtual ICollection<tpProposalLineRevision> tpProposalLineRevisions { get; set; }
        public virtual ICollection<trProposalLineCurrency> trProposalLineCurrencys { get; set; }
        public virtual ICollection<trPurchaseRequisitionProposalConfirmationEMailNotificationDetail> trPurchaseRequisitionProposalConfirmationEMailNotificationDetails { get; set; }
    }
}
