using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCreditType")]
    public partial class bsCreditType
    {
        public bsCreditType()
        {
            bsCreditTypeDescs = new HashSet<bsCreditTypeDesc>();
            cdBankCreditTypes = new HashSet<cdBankCreditType>();
        }

        [Key]
        [Required]
        public byte CreditTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCreditTypeDesc> bsCreditTypeDescs { get; set; }
        public virtual ICollection<cdBankCreditType> cdBankCreditTypes { get; set; }
    }
}
