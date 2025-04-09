using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prProposalConfirmationRuleDepartments")]
    public partial class prProposalConfirmationRuleDepartments
    {
        public prProposalConfirmationRuleDepartments()
        {
        }

        [Key]
        [Required]
        public Guid ProposalConfirmationRuleID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

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
        public virtual cdProposalConfirmationRule cdProposalConfirmationRule { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
