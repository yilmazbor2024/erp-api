using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPurchaseRequisitionProposal")]
    public partial class tpPurchaseRequisitionProposal
    {
        public tpPurchaseRequisitionProposal()
        {
            tpPurchaseRequisitionProposalATAttributes = new HashSet<tpPurchaseRequisitionProposalATAttribute>();
            tpPurchaseRequisitionProposalConfirmations = new HashSet<tpPurchaseRequisitionProposalConfirmation>();
            tpPurchaseRequisitionProposalRevisions = new HashSet<tpPurchaseRequisitionProposalRevision>();
            trPurchaseRequisitionProposalConfirmationEMailNotificationDetails = new HashSet<trPurchaseRequisitionProposalConfirmationEMailNotificationDetail>();
        }

        [Key]
        [Required]
        public Guid PurchaseRequisitionProposalID { get; set; }

        [Required]
        public object ProposalNumber { get; set; }

        [Required]
        public DateTime ProposalDate { get; set; }

        [Required]
        public TimeSpan ProposalTime { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public short PaymentTerm { get; set; }

        [Required]
        public DateTime AverageDueDate { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        public string LineDescription { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CompanyName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

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
        public bool TaxIncluded { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [Required]
        public byte ConfirmationStatusCode { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        public bool? IsClosed { get; set; }

        [Required]
        public bool ClosedBySystem { get; set; }

        [Required]
        public bool RevisionRequested { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

        [Required]
        public Guid PurchaseRequisitionLineID { get; set; }

        public Guid? OrderLineID { get; set; }

        public Guid? InvoiceLineID { get; set; }

        public Guid? ClosedByPurchaseRequisitionProposalID { get; set; }

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
        public virtual trPurchaseRequisitionLine trPurchaseRequisitionLine { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual bsConfirmationStatus bsConfirmationStatus { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpPurchaseRequisitionProposalATAttribute> tpPurchaseRequisitionProposalATAttributes { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposalConfirmation> tpPurchaseRequisitionProposalConfirmations { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposalRevision> tpPurchaseRequisitionProposalRevisions { get; set; }
        public virtual ICollection<trPurchaseRequisitionProposalConfirmationEMailNotificationDetail> trPurchaseRequisitionProposalConfirmationEMailNotificationDetails { get; set; }
    }
}
