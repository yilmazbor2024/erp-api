using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInvoiceLineLinkedProduct")]
    public partial class trInvoiceLineLinkedProduct
    {
        public trInvoiceLineLinkedProduct()
        {
            trInvoiceLines = new HashSet<trInvoiceLine>();
        }

        [Key]
        [Required]
        public Guid InvoiceLineLinkedProductID { get; set; }

        [Required]
        public byte LinkedProductTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LinkedProductCode { get; set; }

        [Required]
        public double LinkedProductQty { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal PriceVI { get; set; }

        [Required]
        public Guid InvoiceHeaderID { get; set; }

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
        public virtual cdItem cdItem { get; set; }

        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
    }
}
