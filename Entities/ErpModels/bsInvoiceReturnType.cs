using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsInvoiceReturnType")]
    public partial class bsInvoiceReturnType
    {
        public bsInvoiceReturnType()
        {
            bsInvoiceReturnTypeDescs = new HashSet<bsInvoiceReturnTypeDesc>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
        }

        [Key]
        [Required]
        public byte InvoiceReturnTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsInvoiceReturnTypeDesc> bsInvoiceReturnTypeDescs { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
    }
}
