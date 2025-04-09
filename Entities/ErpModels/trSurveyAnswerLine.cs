using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSurveyAnswerLine")]
    public partial class trSurveyAnswerLine
    {
        public trSurveyAnswerLine()
        {
        }

        [Key]
        [Required]
        public Guid SurveyAnswerLineID { get; set; }

        [Required]
        public int SurveySectionNumber { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        public Guid? QuestionOptionID { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object AnswerText { get; set; }

        [Required]
        public int AnswerNumeric { get; set; }

        [Required]
        public DateTime AnswerDate { get; set; }

        [Required]
        public bool AnswerYesNo { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object DescriptionForYes { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object DescriptionForNo { get; set; }

        [Required]
        public Guid SurveyAnswerHeaderID { get; set; }

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
        public virtual trSurveyAnswerHeader trSurveyAnswerHeader { get; set; }
        public virtual cdSurveyQuestionOption cdSurveyQuestionOption { get; set; }

    }
}
