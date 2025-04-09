using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsQuestionInputType")]
    public partial class bsQuestionInputType
    {
        public bsQuestionInputType()
        {
            bsQuestionInputTypeDescs = new HashSet<bsQuestionInputTypeDesc>();
            cdSurveyQuestions = new HashSet<cdSurveyQuestion>();
        }

        [Key]
        [Required]
        public byte QuestionInputTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsQuestionInputTypeDesc> bsQuestionInputTypeDescs { get; set; }
        public virtual ICollection<cdSurveyQuestion> cdSurveyQuestions { get; set; }
    }
}
