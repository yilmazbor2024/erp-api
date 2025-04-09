using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trTaxIncurredLine")]
    public partial class trTaxIncurredLine
    {
        public trTaxIncurredLine()
        {
        }

        [Key]
        [Required]
        public Guid TaxIncurredLineID { get; set; }

        [Required]
        public Guid SalesInvoiceLineID { get; set; }

        public Guid? PurchaseInvoiceLineID { get; set; }

        public Guid? InnerLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public Guid TaxIncurredHeaderID { get; set; }

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
        public virtual trTaxIncurredHeader trTaxIncurredHeader { get; set; }
        public virtual trInnerLine trInnerLine { get; set; }

    }
}
