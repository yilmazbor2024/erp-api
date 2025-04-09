using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdConfirmationRule")]
    public partial class cdConfirmationRule
    {
        public cdConfirmationRule()
        {
            cdCurrAccs = new HashSet<cdCurrAcc>();
            prConfirmationRuleSteps = new HashSet<prConfirmationRuleStep>();
        }

        [Key]
        [Required]
        public Guid ConfirmationRuleID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationRuleCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte ConfirmationRuleTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

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
        public virtual bsConfirmationRuleType bsConfirmationRuleType { get; set; }

        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<prConfirmationRuleStep> prConfirmationRuleSteps { get; set; }
    }
}
