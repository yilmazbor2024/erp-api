using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPurchaseRequisitionLine")]
    public partial class trPurchaseRequisitionLine
    {
        public trPurchaseRequisitionLine()
        {
            tpPurchaseRequisitionClosedByInventorys = new HashSet<tpPurchaseRequisitionClosedByInventory>();
            tpPurchaseRequisitionConfirmations = new HashSet<tpPurchaseRequisitionConfirmation>();
            tpPurchaseRequisitionItemAttributeInfos = new HashSet<tpPurchaseRequisitionItemAttributeInfo>();
            tpPurchaseRequisitionItemInfos = new HashSet<tpPurchaseRequisitionItemInfo>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            tpPurchaseRequisitionReceiveInfos = new HashSet<tpPurchaseRequisitionReceiveInfo>();
            tpPurchaseRequisitionRevisions = new HashSet<tpPurchaseRequisitionRevision>();
            tpPurchaseRequisitionTechnicalNotess = new HashSet<tpPurchaseRequisitionTechnicalNotes>();
            tpPurchaseRequisitionTraces = new HashSet<tpPurchaseRequisitionTrace>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalLines = new HashSet<trProposalLine>();
            trPurchaseRequisitionConfirmationEMailNotifications = new HashSet<trPurchaseRequisitionConfirmationEMailNotification>();
            trPurchaseRequisitionProposalConfirmationEMailNotifications = new HashSet<trPurchaseRequisitionProposalConfirmationEMailNotification>();
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionCode { get; set; }

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
        public DateTime NeedByDate { get; set; }

        [Required]
        public TimeSpan NeedByTime { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

        [Required]
        public double Qty { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool IsSampleRequested { get; set; }

        [Required]
        public object DeliveryOfficeCode { get; set; }

        [Required]
        public byte DeliveryStoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeliveryStoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryWarehouseCode { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public bool RevisionRequested { get; set; }

        public bool? IsClosed { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ClosedUserName { get; set; }

        [Required]
        public byte CloseType { get; set; }

        public Guid? RequisitionLimitID { get; set; }

        [Required]
        public Guid PurchaseRequisitionHeaderID { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }
        public virtual trPurchaseRequisitionHeader trPurchaseRequisitionHeader { get; set; }
        public virtual prRequisitionLimit prRequisitionLimit { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdRequisition cdRequisition { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpPurchaseRequisitionClosedByInventory> tpPurchaseRequisitionClosedByInventorys { get; set; }
        public virtual ICollection<tpPurchaseRequisitionConfirmation> tpPurchaseRequisitionConfirmations { get; set; }
        public virtual ICollection<tpPurchaseRequisitionItemAttributeInfo> tpPurchaseRequisitionItemAttributeInfos { get; set; }
        public virtual ICollection<tpPurchaseRequisitionItemInfo> tpPurchaseRequisitionItemInfos { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<tpPurchaseRequisitionReceiveInfo> tpPurchaseRequisitionReceiveInfos { get; set; }
        public virtual ICollection<tpPurchaseRequisitionRevision> tpPurchaseRequisitionRevisions { get; set; }
        public virtual ICollection<tpPurchaseRequisitionTechnicalNotes> tpPurchaseRequisitionTechnicalNotess { get; set; }
        public virtual ICollection<tpPurchaseRequisitionTrace> tpPurchaseRequisitionTraces { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trPurchaseRequisitionConfirmationEMailNotification> trPurchaseRequisitionConfirmationEMailNotifications { get; set; }
        public virtual ICollection<trPurchaseRequisitionProposalConfirmationEMailNotification> trPurchaseRequisitionProposalConfirmationEMailNotifications { get; set; }
    }
}
