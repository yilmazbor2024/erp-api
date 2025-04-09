using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInvoiceLineSum")]
    public partial class trInvoiceLineSum
    {
        public trInvoiceLineSum()
        {
            trInvoiceLineSumDetails = new HashSet<trInvoiceLineSumDetail>();
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

        [Key]
        [Required]
        public int InvoiceLineSumID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public int LotQty { get; set; }

        public decimal? Price { get; set; }

        public decimal? PriceVI { get; set; }

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
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual cdLot cdLot { get; set; }

        public virtual ICollection<trInvoiceLineSumDetail> trInvoiceLineSumDetails { get; set; }
    }
}
