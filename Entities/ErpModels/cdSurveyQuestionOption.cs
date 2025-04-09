using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSurveyQuestionOption")]
    public partial class cdSurveyQuestionOption
    {
        public cdSurveyQuestionOption()
        {
            cdSurveyQuestionOptionDescs = new HashSet<cdSurveyQuestionOptionDesc>();
            trSurveyAnswerLines = new HashSet<trSurveyAnswerLine>();
        }

        [Key]
        [Required]
        public Guid QuestionOptionID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

        [Required]
        public int SurveySectionNumber { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public int Point { get; set; }

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

        // Navigation Properties
        public virtual cdSurveyQuestion cdSurveyQuestion { get; set; }

        public virtual ICollection<cdSurveyQuestionOptionDesc> cdSurveyQuestionOptionDescs { get; set; }
        public virtual ICollection<trSurveyAnswerLine> trSurveyAnswerLines { get; set; }
    }
}
