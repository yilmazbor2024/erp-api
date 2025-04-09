using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSurveyQuestion")]
    public partial class cdSurveyQuestion
    {
        public cdSurveyQuestion()
        {
            cdSurveyQuestionDescs = new HashSet<cdSurveyQuestionDesc>();
            cdSurveyQuestionOptions = new HashSet<cdSurveyQuestionOption>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

        [Key]
        [Required]
        public int SurveySectionNumber { get; set; }

        [Key]
        [Required]
        public int QuestionNumber { get; set; }

        [Required]
        public bool DescriptionRequired { get; set; }

        [Required]
        public bool DescriptionRequiredForYes { get; set; }

        [Required]
        public bool DescriptionRequiredForNo { get; set; }

        [Required]
        public byte QuestionInputTypeCode { get; set; }

        [Required]
        public bool IsRequired { get; set; }

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
        public virtual cdSurveySection cdSurveySection { get; set; }
        public virtual bsQuestionInputType bsQuestionInputType { get; set; }

        public virtual ICollection<cdSurveyQuestionDesc> cdSurveyQuestionDescs { get; set; }
        public virtual ICollection<cdSurveyQuestionOption> cdSurveyQuestionOptions { get; set; }
    }
}
