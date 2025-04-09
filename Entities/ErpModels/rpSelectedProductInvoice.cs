using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpSelectedProductInvoice")]
    public partial class rpSelectedProductInvoice
    {
        public rpSelectedProductInvoice()
        {
        }

        [Key]
        [Required]
        public Guid SelectedProductID { get; set; }

        [Key]
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

    }
}
