using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdTurnoverTargetType")]
    public partial class cdTurnoverTargetType
    {
        public cdTurnoverTargetType()
        {
            cdTurnoverTargetTypeDescs = new HashSet<cdTurnoverTargetTypeDesc>();
            dfMonthlyTurnoverTargets = new HashSet<dfMonthlyTurnoverTarget>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TurnoverTargetTypeCode { get; set; }

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

        public virtual ICollection<cdTurnoverTargetTypeDesc> cdTurnoverTargetTypeDescs { get; set; }
        public virtual ICollection<dfMonthlyTurnoverTarget> dfMonthlyTurnoverTargets { get; set; }
    }
}
