using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInvoiceLineReportedSales")]
    public partial class trInvoiceLineReportedSales
    {
        public trInvoiceLineReportedSales()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceLineID { get; set; }

        public Guid? ReportedSaleLineID { get; set; }

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
        public virtual trReportedSaleLine trReportedSaleLine { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }

    }
}
