using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJobPosition")]
    public partial class cdJobPosition
    {
        public cdJobPosition()
        {
            cdJobPositionDescs = new HashSet<cdJobPositionDesc>();
            hrJobInterviews = new HashSet<hrJobInterview>();
            hrJobInterviewResultss = new HashSet<hrJobInterviewResults>();
            hrJobPositionCandidates = new HashSet<hrJobPositionCandidate>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobPositionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTitleCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public byte SampleJobCandidateTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SampleJobCandidateCode { get; set; }

        [Required]
        public byte AvailablePositionCount { get; set; }

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

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdJobType cdJobType { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }
        public virtual cdJobTitle cdJobTitle { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdJobPositionDesc> cdJobPositionDescs { get; set; }
        public virtual ICollection<hrJobInterview> hrJobInterviews { get; set; }
        public virtual ICollection<hrJobInterviewResults> hrJobInterviewResultss { get; set; }
        public virtual ICollection<hrJobPositionCandidate> hrJobPositionCandidates { get; set; }
    }
}
