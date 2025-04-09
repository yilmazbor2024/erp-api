using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRequisitionConfirmationRule")]
    public partial class cdRequisitionConfirmationRule
    {
        public cdRequisitionConfirmationRule()
        {
            prRequisitionConfirmationRuleDepartmentss = new HashSet<prRequisitionConfirmationRuleDepartments>();
            prRequisitionConfirmationRuleSteps = new HashSet<prRequisitionConfirmationRuleStep>();
        }

        [Key]
        [Required]
        public Guid RequisitionConfirmationRuleID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RequisitionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [Required]
        public byte EmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeCode { get; set; }

        [Required]
        public double Qty { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdRequisition cdRequisition { get; set; }
        public virtual cdRequisitionType cdRequisitionType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prRequisitionConfirmationRuleDepartments> prRequisitionConfirmationRuleDepartmentss { get; set; }
        public virtual ICollection<prRequisitionConfirmationRuleStep> prRequisitionConfirmationRuleSteps { get; set; }
    }
}
