using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSupportRequestSurvey")]
    public partial class trSupportRequestSurvey
    {
        public trSupportRequestSurvey()
        {
        }

        [Key]
        [Required]
        public Guid SupportRequestHeaderID { get; set; }

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
        public virtual trSupportRequestHeader trSupportRequestHeader { get; set; }
        public virtual trSurveyAnswerHeader trSurveyAnswerHeader { get; set; }

    }
}
