using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdJobDepartment")]
    public partial class cdJobDepartment
    {
        public cdJobDepartment()
        {
            cdJobDepartmentDescs = new HashSet<cdJobDepartmentDesc>();
            cdJobPositions = new HashSet<cdJobPosition>();
            cdProposalConfirmationLimits = new HashSet<cdProposalConfirmationLimit>();
            cdProposalConfirmationRules = new HashSet<cdProposalConfirmationRule>();
            cdRequisitionConfirmationLimits = new HashSet<cdRequisitionConfirmationLimit>();
            cdRequisitionConfirmationRules = new HashSet<cdRequisitionConfirmationRule>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            hrEmployeeMonthlySumDetails = new HashSet<hrEmployeeMonthlySumDetail>();
            hrEmployeeWorkPlaces = new HashSet<hrEmployeeWorkPlace>();
            prCompanyExpenseInvoiceConfirmationRules = new HashSet<prCompanyExpenseInvoiceConfirmationRule>();
            prConfirmationRuleStepUsers = new HashSet<prConfirmationRuleStepUser>();
            prEmployeeWorkplaceInformations = new HashSet<prEmployeeWorkplaceInformation>();
            prExpenseInvoiceConfirmationRules = new HashSet<prExpenseInvoiceConfirmationRule>();
            prProposalConfirmationRuleDepartmentss = new HashSet<prProposalConfirmationRuleDepartments>();
            prProposalConfirmationRuleStepUsers = new HashSet<prProposalConfirmationRuleStepUser>();
            prRequisitionConfirmationRuleDepartmentss = new HashSet<prRequisitionConfirmationRuleDepartments>();
            prRequisitionConfirmationRuleStepUsers = new HashSet<prRequisitionConfirmationRuleStepUser>();
            prRequisitionLimits = new HashSet<prRequisitionLimit>();
            prWorkPlaceGLAccss = new HashSet<prWorkPlaceGLAccs>();
            prWorkPlaceOptimalEmployments = new HashSet<prWorkPlaceOptimalEmployment>();
            trPayrollLines = new HashSet<trPayrollLine>();
            trPurchaseRequisitionHeaders = new HashSet<trPurchaseRequisitionHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [Required]
        public double ShortTermInsuranceRateLevel { get; set; }

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

        public virtual ICollection<cdJobDepartmentDesc> cdJobDepartmentDescs { get; set; }
        public virtual ICollection<cdJobPosition> cdJobPositions { get; set; }
        public virtual ICollection<cdProposalConfirmationLimit> cdProposalConfirmationLimits { get; set; }
        public virtual ICollection<cdProposalConfirmationRule> cdProposalConfirmationRules { get; set; }
        public virtual ICollection<cdRequisitionConfirmationLimit> cdRequisitionConfirmationLimits { get; set; }
        public virtual ICollection<cdRequisitionConfirmationRule> cdRequisitionConfirmationRules { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<hrEmployeeMonthlySumDetail> hrEmployeeMonthlySumDetails { get; set; }
        public virtual ICollection<hrEmployeeWorkPlace> hrEmployeeWorkPlaces { get; set; }
        public virtual ICollection<prCompanyExpenseInvoiceConfirmationRule> prCompanyExpenseInvoiceConfirmationRules { get; set; }
        public virtual ICollection<prConfirmationRuleStepUser> prConfirmationRuleStepUsers { get; set; }
        public virtual ICollection<prEmployeeWorkplaceInformation> prEmployeeWorkplaceInformations { get; set; }
        public virtual ICollection<prExpenseInvoiceConfirmationRule> prExpenseInvoiceConfirmationRules { get; set; }
        public virtual ICollection<prProposalConfirmationRuleDepartments> prProposalConfirmationRuleDepartmentss { get; set; }
        public virtual ICollection<prProposalConfirmationRuleStepUser> prProposalConfirmationRuleStepUsers { get; set; }
        public virtual ICollection<prRequisitionConfirmationRuleDepartments> prRequisitionConfirmationRuleDepartmentss { get; set; }
        public virtual ICollection<prRequisitionConfirmationRuleStepUser> prRequisitionConfirmationRuleStepUsers { get; set; }
        public virtual ICollection<prRequisitionLimit> prRequisitionLimits { get; set; }
        public virtual ICollection<prWorkPlaceGLAccs> prWorkPlaceGLAccss { get; set; }
        public virtual ICollection<prWorkPlaceOptimalEmployment> prWorkPlaceOptimalEmployments { get; set; }
        public virtual ICollection<trPayrollLine> trPayrollLines { get; set; }
        public virtual ICollection<trPurchaseRequisitionHeader> trPurchaseRequisitionHeaders { get; set; }
    }
}
