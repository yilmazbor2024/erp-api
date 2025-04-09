using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trReserveLine")]
    public partial class trReserveLine
    {
        public trReserveLine()
        {
            trDispOrderLines = new HashSet<trDispOrderLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trPickingLines = new HashSet<trPickingLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
        }

        [Key]
        [Required]
        public Guid ReserveLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [Required]
        public double In_Qty1 { get; set; }

        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        public DateTime? OrderDeliveryDate { get; set; }

        [Required]
        public Guid OrderLineID { get; set; }

        public Guid? PurchaseOrderLineID { get; set; }

        [Required]
        public Guid ReserveHeaderID { get; set; }

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
        public virtual trReserveHeader trReserveHeader { get; set; }
        public virtual trOrderLine trOrderLine { get; set; }

        public virtual ICollection<trDispOrderLine> trDispOrderLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trPickingLine> trPickingLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
    }
}
