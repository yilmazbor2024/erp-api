using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInvoiceLineSumDetail")]
    public partial class trInvoiceLineSumDetail
    {
        public trInvoiceLineSumDetail()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceHeaderID { get; set; }

   
        [Key]
        [Required]
        public int InvoiceLineSumID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public int LotQty { get; set; }

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
        public virtual cdLot cdLot { get; set; }
        public virtual trInvoiceHeader trInvoiceHeader { get; set; }
        public virtual trInvoiceLineSum trInvoiceLineSum { get; set; }
        public virtual cdColor cdColor { get; set; }

    }
}
