using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSurvey")]
    public partial class cdSurvey
    {
        public cdSurvey()
        {
            cdSurveyDescs = new HashSet<cdSurveyDesc>();
            cdSurveySections = new HashSet<cdSurveySection>();
            dfCreditableConfirmations = new HashSet<dfCreditableConfirmation>();
            dfSupportRequestSurveyDefaults = new HashSet<dfSupportRequestSurveyDefault>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContactTypeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Instructions { get; set; }

        [Required]
        public bool UseStore { get; set; }

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
        public virtual bsCurrAccType bsCurrAccType { get; set; }

        public virtual ICollection<cdSurveyDesc> cdSurveyDescs { get; set; }
        public virtual ICollection<cdSurveySection> cdSurveySections { get; set; }
        public virtual ICollection<dfCreditableConfirmation> dfCreditableConfirmations { get; set; }
        public virtual ICollection<dfSupportRequestSurveyDefault> dfSupportRequestSurveyDefaults { get; set; }
    }
}
