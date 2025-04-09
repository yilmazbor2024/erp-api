using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPrevJobType")]
    public partial class cdPrevJobType
    {
        public cdPrevJobType()
        {
            cdPrevJobTypeDescs = new HashSet<cdPrevJobTypeDesc>();
            prEmployeePrevJobs = new HashSet<prEmployeePrevJob>();
        }

        [Key]
        [Required]
        public byte PrevJobTypeCode { get; set; }

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

        public virtual ICollection<cdPrevJobTypeDesc> cdPrevJobTypeDescs { get; set; }
        public virtual ICollection<prEmployeePrevJob> prEmployeePrevJobs { get; set; }
    }
}
