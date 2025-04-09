using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDebtStatusType")]
    public partial class bsDebtStatusType
    {
        public bsDebtStatusType()
        {
            bsDebtStatusTypeDescs = new HashSet<bsDebtStatusTypeDesc>();
            prCurrAccBadDebtStatuss = new HashSet<prCurrAccBadDebtStatus>();
            trBadDebtTransLineResults = new HashSet<trBadDebtTransLineResult>();
        }

        [Key]
        [Required]
        public byte DebtStatusTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDebtStatusTypeDesc> bsDebtStatusTypeDescs { get; set; }
        public virtual ICollection<prCurrAccBadDebtStatus> prCurrAccBadDebtStatuss { get; set; }
        public virtual ICollection<trBadDebtTransLineResult> trBadDebtTransLineResults { get; set; }
    }
}
