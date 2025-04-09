using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTaxType")]
    public partial class bsTaxType
    {
        public bsTaxType()
        {
            bsTaxTypeDescs = new HashSet<bsTaxTypeDesc>();
            dfAvailableTaxTypesOnPoss = new HashSet<dfAvailableTaxTypesOnPos>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
        }

        [Key]
        [Required]
        public byte TaxTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsTaxTypeDesc> bsTaxTypeDescs { get; set; }
        public virtual ICollection<dfAvailableTaxTypesOnPos> dfAvailableTaxTypesOnPoss { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
    }
}
