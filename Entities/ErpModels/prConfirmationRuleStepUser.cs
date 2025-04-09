using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prConfirmationRuleStepUser")]
    public partial class prConfirmationRuleStepUser
    {
        public prConfirmationRuleStepUser()
        {
        }

        [Key]
        [Required]
        public Guid ConfirmationRuleStepUserID { get; set; }

        [Required]
        public Guid ConfirmationRuleID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool ConfirmationByDepartmentManager { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

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
        public virtual prConfirmationRuleStep prConfirmationRuleStep { get; set; }
        public virtual cdWorkPlace cdWorkPlace { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
