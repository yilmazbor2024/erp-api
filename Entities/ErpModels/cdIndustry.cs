using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdIndustry")]
    public partial class cdIndustry
    {
        public cdIndustry()
        {
            cdIndustryDescs = new HashSet<cdIndustryDesc>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
            prEmployeePrevJobs = new HashSet<prEmployeePrevJob>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IndustryCode { get; set; }

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

        public virtual ICollection<cdIndustryDesc> cdIndustryDescs { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
        public virtual ICollection<prEmployeePrevJob> prEmployeePrevJobs { get; set; }
    }
}
