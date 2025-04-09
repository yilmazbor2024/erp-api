using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stItemSerialNumber")]
    public partial class stItemSerialNumber
    {
        public stItemSerialNumber()
        {
        }

        [Key]
        [Required]
        public Guid ItemSerialNumberID { get; set; }

        public Guid? ShipmentLineID { get; set; }

        public Guid? InvoiceLineID { get; set; }

        public Guid? ReportedSaleLineID { get; set; }

        public Guid? InnerLineID { get; set; }

        public Guid? StockID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SerialNumber { get; set; }

        // Navigation Properties
        public virtual trReportedSaleLine trReportedSaleLine { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual trInnerLine trInnerLine { get; set; }
        public virtual trStock trStock { get; set; }
        public virtual trShipmentLine trShipmentLine { get; set; }

    }
}
