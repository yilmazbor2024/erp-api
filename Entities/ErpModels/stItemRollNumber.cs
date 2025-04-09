using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stItemRollNumber")]
    public partial class stItemRollNumber
    {
        public stItemRollNumber()
        {
        }

        [Key]
        [Required]
        public Guid ItemRollNumberID { get; set; }

        public Guid? ShipmentLineID { get; set; }

        public Guid? InvoiceLineID { get; set; }

        public Guid? InnerLineID { get; set; }

        public Guid? StockID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RollNumber { get; set; }

        [Required]
        public double Qty { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchGroupCode { get; set; }

        // Navigation Properties
        public virtual trInvoiceLine trInvoiceLine { get; set; }
        public virtual cdRoll cdRoll { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual trInnerLine trInnerLine { get; set; }
        public virtual cdBatchGroup cdBatchGroup { get; set; }
        public virtual trStock trStock { get; set; }
        public virtual trShipmentLine trShipmentLine { get; set; }

    }
}
