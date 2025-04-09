using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfCreditableConfirmation")]
    public partial class dfCreditableConfirmation
    {
        public dfCreditableConfirmation()
        {
        }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public decimal MinOrderAmountForConfirmation { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SurveyCode { get; set; }

        [Required]
        public int MinSurveyPoint { get; set; }

        [Required]
        public bool ReSurveyEachOrder { get; set; }

        [Required]
        public bool ReSurveyOverdueDebits { get; set; }

        [Required]
        public decimal MaxOrderAmountForReSurvey { get; set; }

        [Required]
        public int SurveyExpireDay { get; set; }

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

    }
}
