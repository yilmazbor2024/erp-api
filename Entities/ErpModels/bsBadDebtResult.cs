using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBadDebtResult")]
    public partial class bsBadDebtResult
    {
        public bsBadDebtResult()
        {
            bsBadDebtResultDescs = new HashSet<bsBadDebtResultDesc>();
        }

        [Key]
        [Required]
        public byte BadDebtResultCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBadDebtResultDesc> bsBadDebtResultDescs { get; set; }
    }
}
