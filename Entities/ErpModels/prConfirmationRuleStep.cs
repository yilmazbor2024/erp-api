using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prConfirmationRuleStep")]
    public partial class prConfirmationRuleStep
    {
        public prConfirmationRuleStep()
        {
            prConfirmationRuleStepUsers = new HashSet<prConfirmationRuleStepUser>();
        }

        [Key]
        [Required]
        public Guid ConfirmationRuleID { get; set; }

        [Key]
        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public int ExpireTimeForNextStep { get; set; }

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
        public virtual cdConfirmationRule cdConfirmationRule { get; set; }

        public virtual ICollection<prConfirmationRuleStepUser> prConfirmationRuleStepUsers { get; set; }
    }
}
