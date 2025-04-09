using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBadDebtTransType")]
    public partial class bsBadDebtTransType
    {
        public bsBadDebtTransType()
        {
            bsBadDebtTransTypeDescs = new HashSet<bsBadDebtTransTypeDesc>();
            trBadDebtTransHeaders = new HashSet<trBadDebtTransHeader>();
        }

        [Key]
        [Required]
        public byte BadDebtTransTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBadDebtTransTypeDesc> bsBadDebtTransTypeDescs { get; set; }
        public virtual ICollection<trBadDebtTransHeader> trBadDebtTransHeaders { get; set; }
    }
}
