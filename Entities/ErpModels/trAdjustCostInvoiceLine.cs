using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAdjustCostInvoiceLine")]
    public partial class trAdjustCostInvoiceLine
    {
        public trAdjustCostInvoiceLine()
        {
        }

        [Key]
        [Required]
        public Guid AdjustCostHeaderID { get; set; }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Key]
        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public decimal IncreaseAmount { get; set; }

        [Required]
        public decimal ReduceAmount { get; set; }

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
        public virtual trAdjustCostInvoice trAdjustCostInvoice { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }

    }
}
