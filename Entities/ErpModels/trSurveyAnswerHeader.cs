using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trSurveyAnswerHeader")]
    public partial class trSurveyAnswerHeader
    {
        public trSurveyAnswerHeader()
        {
            trOrderAuditorSurveys = new HashSet<trOrderAuditorSurvey>();
            trOrderSurveys = new HashSet<trOrderSurvey>();
            trSupportRequestSurveys = new HashSet<trSupportRequestSurvey>();
            trSurveyAnswerLines = new HashSet<trSurveyAnswerLine>();
        }

        [Key]
        [Required]
        public Guid SurveyAnswerHeaderID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? ContactID { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

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

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

        // Navigation Properties
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trOrderAuditorSurvey> trOrderAuditorSurveys { get; set; }
        public virtual ICollection<trOrderSurvey> trOrderSurveys { get; set; }
        public virtual ICollection<trSupportRequestSurvey> trSupportRequestSurveys { get; set; }
        public virtual ICollection<trSurveyAnswerLine> trSurveyAnswerLines { get; set; }
    }
}
