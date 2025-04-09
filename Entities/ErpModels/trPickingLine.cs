using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPickingLine")]
    public partial class trPickingLine
    {
        public trPickingLine()
        {
            stItemRollNumberPickings = new HashSet<stItemRollNumberPicking>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
        }

        [Key]
        [Required]
        public Guid PickingLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UsedBarcode { get; set; }

        public DateTime? OrderDeliveryDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SectionCode { get; set; }

        [Required]
        public Guid OrderLineID { get; set; }

        public Guid? ReserveLineID { get; set; }

        public Guid? DispOrderLineID { get; set; }

        [Required]
        public Guid PickingHeaderID { get; set; }

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
        public virtual trOrderLine trOrderLine { get; set; }
        public virtual trDispOrderLine trDispOrderLine { get; set; }
        public virtual trPickingHeader trPickingHeader { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual trReserveLine trReserveLine { get; set; }

        public virtual ICollection<stItemRollNumberPicking> stItemRollNumberPickings { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
    }
}
