using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerLine")]
    public partial class trInnerLine
    {
        public trInnerLine()
        {
            stItemRollNumbers = new HashSet<stItemRollNumber>();
            stItemSerialNumbers = new HashSet<stItemSerialNumber>();
            tpInnerCustomsTransferImportInvoiceLines = new HashSet<tpInnerCustomsTransferImportInvoiceLine>();
            tpInnerITAttributes = new HashSet<tpInnerITAttribute>();
            tpInnerLineDocuments = new HashSet<tpInnerLineDocument>();
            tpInnerLinePurchaseInvoiceLines = new HashSet<tpInnerLinePurchaseInvoiceLine>();
            trAdjustCostInnerLines = new HashSet<trAdjustCostInnerLine>();
            trInnerLineBOMs = new HashSet<trInnerLineBOM>();
            trInnerLineCostCenterRatess = new HashSet<trInnerLineCostCenterRates>();
            trInnerLineGiftCards = new HashSet<trInnerLineGiftCard>();
            trInnerLineInventoryTransfers = new HashSet<trInnerLineInventoryTransfer>();
            trItemTestLines = new HashSet<trItemTestLine>();
            trTaxIncurredLines = new HashSet<trTaxIncurredLine>();
        }

        [Key]
        [Required]
        public Guid InnerLineID { get; set; }

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

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SectionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal CostAmount { get; set; }

        [Required]
        public decimal CostPriceWithInflation { get; set; }

        [Required]
        public decimal CostAmountWithInflation { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ScrapReasonCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        public DateTime? ManufactureDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Guid? SupportRequestHeaderID { get; set; }

        [Required]
        public Guid InnerHeaderID { get; set; }

        [Required]
        public int InnerLineSumID { get; set; }

        [Required]
        public int InnerLineSerialSumID { get; set; }

        public Guid? InnerOrderLineID { get; set; }

        public int? InnerLineBOMID { get; set; }

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
        public virtual trInnerHeader trInnerHeader { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trInnerOrderLine trInnerOrderLine { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdScrapReason cdScrapReason { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }

        public virtual ICollection<stItemRollNumber> stItemRollNumbers { get; set; }
        public virtual ICollection<stItemSerialNumber> stItemSerialNumbers { get; set; }
        public virtual ICollection<tpInnerCustomsTransferImportInvoiceLine> tpInnerCustomsTransferImportInvoiceLines { get; set; }
        public virtual ICollection<tpInnerITAttribute> tpInnerITAttributes { get; set; }
        public virtual ICollection<tpInnerLineDocument> tpInnerLineDocuments { get; set; }
        public virtual ICollection<tpInnerLinePurchaseInvoiceLine> tpInnerLinePurchaseInvoiceLines { get; set; }
        public virtual ICollection<trAdjustCostInnerLine> trAdjustCostInnerLines { get; set; }
        public virtual ICollection<trInnerLineBOM> trInnerLineBOMs { get; set; }
        public virtual ICollection<trInnerLineCostCenterRates> trInnerLineCostCenterRatess { get; set; }
        public virtual ICollection<trInnerLineGiftCard> trInnerLineGiftCards { get; set; }
        public virtual ICollection<trInnerLineInventoryTransfer> trInnerLineInventoryTransfers { get; set; }
        public virtual ICollection<trItemTestLine> trItemTestLines { get; set; }
        public virtual ICollection<trTaxIncurredLine> trTaxIncurredLines { get; set; }
    }
}
