using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJobInterviewResult")]
    public partial class cdJobInterviewResult
    {
        public cdJobInterviewResult()
        {
            cdJobInterviewResultDescs = new HashSet<cdJobInterviewResultDesc>();
            hrJobInterviews = new HashSet<hrJobInterview>();
            hrJobInterviewResultss = new HashSet<hrJobInterviewResults>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobInterviewResultCode { get; set; }

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

        public virtual ICollection<cdJobInterviewResultDesc> cdJobInterviewResultDescs { get; set; }
        public virtual ICollection<hrJobInterview> hrJobInterviews { get; set; }
        public virtual ICollection<hrJobInterviewResults> hrJobInterviewResultss { get; set; }
    }
}
