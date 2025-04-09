using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBadDebtReason")]
    public partial class cdBadDebtReason
    {
        public cdBadDebtReason()
        {
            cdBadDebtReasonDescs = new HashSet<cdBadDebtReasonDesc>();
            prCurrAccBadDebtStatuss = new HashSet<prCurrAccBadDebtStatus>();
            trBadDebtTransLines = new HashSet<trBadDebtTransLine>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string BadDebtReasonCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdBadDebtReasonDesc> cdBadDebtReasonDescs { get; set; }
        public virtual ICollection<prCurrAccBadDebtStatus> prCurrAccBadDebtStatuss { get; set; }
        public virtual ICollection<trBadDebtTransLine> trBadDebtTransLines { get; set; }
    }
}
