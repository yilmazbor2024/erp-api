using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSurveySection")]
    public partial class cdSurveySection
    {
        public cdSurveySection()
        {
            auSurveySectionPermits = new HashSet<auSurveySectionPermit>();
            cdSurveyQuestions = new HashSet<cdSurveyQuestion>();
            cdSurveySectionDescs = new HashSet<cdSurveySectionDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

        [Key]
        [Required]
        public int SurveySectionNumber { get; set; }

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
        public virtual cdSurvey cdSurvey { get; set; }

        public virtual ICollection<auSurveySectionPermit> auSurveySectionPermits { get; set; }
        public virtual ICollection<cdSurveyQuestion> cdSurveyQuestions { get; set; }
        public virtual ICollection<cdSurveySectionDesc> cdSurveySectionDescs { get; set; }
    }
}
