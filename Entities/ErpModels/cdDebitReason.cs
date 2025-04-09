using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDebitReason")]
    public partial class cdDebitReason
    {
        public cdDebitReason()
        {
            cdDebitReasonDescs = new HashSet<cdDebitReasonDesc>();
            trDebitLines = new HashSet<trDebitLine>();
            trVirementLines = new HashSet<trVirementLine>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DebitReasonCode { get; set; }

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

        public virtual ICollection<cdDebitReasonDesc> cdDebitReasonDescs { get; set; }
        public virtual ICollection<trDebitLine> trDebitLines { get; set; }
        public virtual ICollection<trVirementLine> trVirementLines { get; set; }
    }
}
