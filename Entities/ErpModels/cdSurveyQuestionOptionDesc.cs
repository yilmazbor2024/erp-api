using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSurveyQuestionOptionDesc")]
    public partial class cdSurveyQuestionOptionDesc
    {
        public cdSurveyQuestionOptionDesc()
        {
        }

        [Key]
        [Required]
        public Guid QuestionOptionID { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string SurveyQuestionOptionDescription { get; set; }

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
        public virtual cdSurveyQuestionOption cdSurveyQuestionOption { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
