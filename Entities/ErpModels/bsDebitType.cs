using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDebitType")]
    public partial class bsDebitType
    {
        public bsDebitType()
        {
            bsDebitTypeDescs = new HashSet<bsDebitTypeDesc>();
            srRefNumberDebits = new HashSet<srRefNumberDebit>();
            trDebitHeaders = new HashSet<trDebitHeader>();
        }

        [Key]
        [Required]
        public byte DebitTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDebitTypeDesc> bsDebitTypeDescs { get; set; }
        public virtual ICollection<srRefNumberDebit> srRefNumberDebits { get; set; }
        public virtual ICollection<trDebitHeader> trDebitHeaders { get; set; }
    }
}
