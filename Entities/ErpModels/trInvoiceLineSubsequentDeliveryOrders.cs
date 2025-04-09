using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInvoiceLineSubsequentDeliveryOrders")]
    public partial class trInvoiceLineSubsequentDeliveryOrders
    {
        public trInvoiceLineSubsequentDeliveryOrders()
        {
        }

        [Key]
        [Required]
        public Guid InvoiceLineID { get; set; }

        [Required]
        public Guid OrderLineID { get; set; }

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
        public virtual trOrderLine trOrderLine { get; set; }

    }
}
