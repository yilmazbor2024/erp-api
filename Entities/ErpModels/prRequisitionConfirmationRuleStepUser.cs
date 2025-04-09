using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prRequisitionConfirmationRuleStepUser")]
    public partial class prRequisitionConfirmationRuleStepUser
    {
        public prRequisitionConfirmationRuleStepUser()
        {
        }

        [Key]
        [Required]
        public Guid RequisitionConfirmationRuleStepUserID { get; set; }

        [Required]
        public Guid RequisitionConfirmationRuleID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool ConfirmationByDepartmentManager { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string UserGroupCode { get; set; }

        [StringLength(30)]
        [Column(TypeName = "Char30")]
        public string UserName { get; set; }

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
        public virtual prRequisitionConfirmationRuleStep prRequisitionConfirmationRuleStep { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
