using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdMessageReason")]
    public partial class cdMessageReason
    {
        public cdMessageReason()
        {
            cdMessageReasonDescs = new HashSet<cdMessageReasonDesc>();
            dfPeriodicalSMSRules = new HashSet<dfPeriodicalSMSRule>();
            trSMSPoolHeaders = new HashSet<trSMSPoolHeader>();
        }

        [Key]
        [Required]
        public int MessageReasonCode { get; set; }

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

        public virtual ICollection<cdMessageReasonDesc> cdMessageReasonDescs { get; set; }
        public virtual ICollection<dfPeriodicalSMSRule> dfPeriodicalSMSRules { get; set; }
        public virtual ICollection<trSMSPoolHeader> trSMSPoolHeaders { get; set; }
    }
}
