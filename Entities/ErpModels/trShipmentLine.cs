using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trShipmentLine")]
    public partial class trShipmentLine
    {
        public trShipmentLine()
        {
            stItemRollNumbers = new HashSet<stItemRollNumber>();
            stItemSerialNumbers = new HashSet<stItemSerialNumber>();
            tpShipmentITAttributes = new HashSet<tpShipmentITAttribute>();
            tpShipmentLinePickingDetailss = new HashSet<tpShipmentLinePickingDetails>();
            tpShipmentReturns = new HashSet<tpShipmentReturn>();
            tpVehicleLoadingLineDeliveryStatuss = new HashSet<tpVehicleLoadingLineDeliveryStatus>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trShipmentLineBOMs = new HashSet<trShipmentLineBOM>();
            trShipmentLineGiftCards = new HashSet<trShipmentLineGiftCard>();
            trVehicleUnLoadingLines = new HashSet<trVehicleUnLoadingLine>();
        }

        [Key]
        [Required]
        public Guid ShipmentLineID { get; set; }

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

        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        public DateTime? OrderDeliveryDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DeliveryCompanyBarcode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LogisticsPackageNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        public DateTime? ManufactureDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Guid? ReserveLineID { get; set; }

        public Guid? DispOrderLineID { get; set; }

        public Guid? PickingLineID { get; set; }

        public Guid? OrderAsnLineID { get; set; }

        public Guid? OrderLineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceCurrencyCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Guid? PriceListLineID { get; set; }

        [Required]
        public bool IsInvoiced { get; set; }

        public Guid? SupportRequestHeaderID { get; set; }

        [Required]
        public Guid ShipmentHeaderID { get; set; }

        [Required]
        public int ShipmentLineSumID { get; set; }

        [Required]
        public int ShipmentLineSerialSumID { get; set; }

        public int? ShipmentLineBOMID { get; set; }

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
        public virtual cdReturnReason cdReturnReason { get; set; }
        public virtual trPickingLine trPickingLine { get; set; }
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual trOrderAsnLine trOrderAsnLine { get; set; }
        public virtual trDispOrderLine trDispOrderLine { get; set; }
        public virtual trPriceListLine trPriceListLine { get; set; }
        public virtual cdSalesperson cdSalesperson { get; set; }
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }
        public virtual trShipmentHeader trShipmentHeader { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual cdPurchasePlan cdPurchasePlan { get; set; }
        public virtual trReserveLine trReserveLine { get; set; }
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }

        public virtual ICollection<stItemRollNumber> stItemRollNumbers { get; set; }
        public virtual ICollection<stItemSerialNumber> stItemSerialNumbers { get; set; }
        public virtual ICollection<tpShipmentITAttribute> tpShipmentITAttributes { get; set; }
        public virtual ICollection<tpShipmentLinePickingDetails> tpShipmentLinePickingDetailss { get; set; }
        public virtual ICollection<tpShipmentReturn> tpShipmentReturns { get; set; }
        public virtual ICollection<tpVehicleLoadingLineDeliveryStatus> tpVehicleLoadingLineDeliveryStatuss { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trShipmentLineBOM> trShipmentLineBOMs { get; set; }
        public virtual ICollection<trShipmentLineGiftCard> trShipmentLineGiftCards { get; set; }
        public virtual ICollection<trVehicleUnLoadingLine> trVehicleUnLoadingLines { get; set; }
    }
}
