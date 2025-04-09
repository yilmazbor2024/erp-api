using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpInvoiceDiscountOfferContributor")]
    public partial class tpInvoiceDiscountOfferContributor
    {
        public tpInvoiceDiscountOfferContributor()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceDiscountOfferContributorID { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

        public Guid? InvoiceLineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

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
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }

    }
}
