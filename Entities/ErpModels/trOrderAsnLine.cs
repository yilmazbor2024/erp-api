using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderAsnLine")]
    public partial class trOrderAsnLine
    {
        public trOrderAsnLine()
        {
            trAllocationProducts = new HashSet<trAllocationProduct>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
        }

        [Key]
        [Required]
        public Guid OrderAsnLineID { get; set; }

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

        public DateTime? OrderDeliveryDate { get; set; }

        [Required]
        public object PickingNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SizeUnitOfMeasureCode { get; set; }

        [Required]
        public float PackedWidth { get; set; }

        [Required]
        public float PackedLength { get; set; }

        [Required]
        public float PackedHeight { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WeightUnitOfMeasureCode { get; set; }

        [Required]
        public float PackedWeight { get; set; }

        [Required]
        public Guid OrderLineID { get; set; }

        [Required]
        public Guid OrderAsnHeaderID { get; set; }

        [Required]
        public int OrderAsnLineSumID { get; set; }

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
        public virtual prItemVariant prItemVariant { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual trOrderAsnHeader trOrderAsnHeader { get; set; }

        public virtual ICollection<trAllocationProduct> trAllocationProducts { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
    }
}
