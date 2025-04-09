using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEInvoiceStatus")]
    public partial class bsEInvoiceStatus
    {
        public bsEInvoiceStatus()
        {
            bsEInvoiceStatusDescs = new HashSet<bsEInvoiceStatusDesc>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
        }

        [Key]
        [Required]
        public byte EInvoiceStatusCode { get; set; }

        [Required]
        public byte InvoiceTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsInvoiceType bsInvoiceType { get; set; }

        public virtual ICollection<bsEInvoiceStatusDesc> bsEInvoiceStatusDescs { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
    }
}
