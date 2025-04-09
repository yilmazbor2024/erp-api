using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("hrJobInterview")]
    public partial class hrJobInterview
    {
        public hrJobInterview()
        {
        }

        [Key]
        [Required]
        public Guid JobInterviewID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

 

        [Required]
        public byte JobCandidateTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string JobCandidateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobPositionCode { get; set; }

        [Required]
        public byte EmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string InterviewPlace { get; set; }

        [Required]
        public DateTime InterviewDate { get; set; }

        [Required]
        public TimeSpan InterviewTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobInterviewResultCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        public string Notes { get; set; }

        public string PlainText { get; set; }

        [Required]
        public bool IsClosed { get; set; }

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

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual cdJobPosition cdJobPosition { get; set; }
        public virtual cdJobInterviewResult cdJobInterviewResult { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
