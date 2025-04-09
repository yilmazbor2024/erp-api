using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prRequisitionConfirmationRuleDepartments")]
    public partial class prRequisitionConfirmationRuleDepartments
    {
        public prRequisitionConfirmationRuleDepartments()
        {
        }

        [Key]
        [Required]
        public Guid RequisitionConfirmationRuleID { get; set; }

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
        public virtual cdRequisitionConfirmationRule cdRequisitionConfirmationRule { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
