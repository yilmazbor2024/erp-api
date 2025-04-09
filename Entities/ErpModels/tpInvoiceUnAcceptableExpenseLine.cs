using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceUnAcceptableExpenseLine")]
    public partial class tpInvoiceUnAcceptableExpenseLine
    {
        public tpInvoiceUnAcceptableExpenseLine()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public Guid UnAcceptableExpenseInvoiceLineID { get; set; }

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
        public virtual trInvoiceLine trInvoiceLine { get; set; }

    }
}
