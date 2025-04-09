using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("hrEmployeePayrollProfile")]
    public partial class hrEmployeePayrollProfile
    {
        public hrEmployeePayrollProfile()
        {
            hrEmployeeAGIs = new HashSet<hrEmployeeAGI>();
            hrEmployeeJobTitles = new HashSet<hrEmployeeJobTitle>();
            hrEmployeeMonthlySums = new HashSet<hrEmployeeMonthlySum>();
            hrEmployeeMonthlySumDetails = new HashSet<hrEmployeeMonthlySumDetail>();
            hrEmployeePrivateInsurances = new HashSet<hrEmployeePrivateInsurance>();
            hrEmployeeSGKBorrowings = new HashSet<hrEmployeeSGKBorrowing>();
            hrEmployeeWages = new HashSet<hrEmployeeWage>();
            hrEmployeeWorkPlaces = new HashSet<hrEmployeeWorkPlace>();
            hrWageGarnishments = new HashSet<hrWageGarnishment>();
            trEmployeeDebits = new HashSet<trEmployeeDebit>();
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [Required]
        public object CompanyCode { get; set; }
 
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeIDNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TransferredEmployeeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SocialInsuranceNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SGKProfessionCode { get; set; }

        [Required]
        public byte EmployeeSocialInsuranceStatusCode { get; set; }

        [Required]
        public bool HaveUnemploymentInsurance { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EmploymentLawCode { get; set; }

        [Required]
        public byte EmployeeTaxStatusCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTypeCode { get; set; }

        [Required]
        public byte EmployeeSpecialTypeCode { get; set; }

        public bool? SignOff { get; set; }

        [Required]
        public DateTime JobEndDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ResignationCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LegalResignationCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LegalResignationLocalCode { get; set; }

        [Required]
        public DateTime EstimatedPensionDate { get; set; }

        [Required]
        public bool IsUnionMember { get; set; }

        [Required]
        public byte QualifyingChildCount { get; set; }

        [Required]
        public byte QualifyingChildCount06 { get; set; }

        [Required]
        public bool BenefitByAgi { get; set; }

        [Required]
        public bool BenefitByAgiForSpouse { get; set; }

        [Required]
        public byte BenefitByAgiChildCount { get; set; }

        [Required]
        public bool UseEmployeeContactsForAgiInfo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeEarningsGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeDebitGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeWorkAdvanceGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeTaxRefundGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SeveranceGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string TerminationGLAccCode { get; set; }

        [Required]
        public bool CompulsoryPensionInsuranceDeductionEnable { get; set; }

        [Required]
        public bool UseDifferentCPIDeductionRate { get; set; }

        [Required]
        public float CPIDeductionRate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKInsuaranceTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string SGKMissionCode { get; set; }

        [Required]
        public float MaxGarnishmentRate { get; set; }

        [Required]
        public bool NotApplyMinWageTaxRelief { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsSGKMission bsSGKMission { get; set; }
        public virtual cdLegalResignation cdLegalResignation { get; set; }
        public virtual cdLegalResignationLocal cdLegalResignationLocal { get; set; }
        public virtual bsSGKInsuaranceType bsSGKInsuaranceType { get; set; }
        public virtual bsEmployeeSpecialType bsEmployeeSpecialType { get; set; }
        public virtual cdEmployeeTaxStatus cdEmployeeTaxStatus { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdJobType cdJobType { get; set; }
        public virtual cdEmployeeSocialInsuranceStatus cdEmployeeSocialInsuranceStatus { get; set; }
        public virtual cdSGKProfession cdSGKProfession { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdEmploymentLaw cdEmploymentLaw { get; set; }
        public virtual cdResignation cdResignation { get; set; }

        public virtual ICollection<hrEmployeeAGI> hrEmployeeAGIs { get; set; }
        public virtual ICollection<hrEmployeeJobTitle> hrEmployeeJobTitles { get; set; }
        public virtual ICollection<hrEmployeeMonthlySum> hrEmployeeMonthlySums { get; set; }
        public virtual ICollection<hrEmployeeMonthlySumDetail> hrEmployeeMonthlySumDetails { get; set; }
        public virtual ICollection<hrEmployeePrivateInsurance> hrEmployeePrivateInsurances { get; set; }
        public virtual ICollection<hrEmployeeSGKBorrowing> hrEmployeeSGKBorrowings { get; set; }
        public virtual ICollection<hrEmployeeWage> hrEmployeeWages { get; set; }
        public virtual ICollection<hrEmployeeWorkPlace> hrEmployeeWorkPlaces { get; set; }
        public virtual ICollection<hrWageGarnishment> hrWageGarnishments { get; set; }
        public virtual ICollection<trEmployeeDebit> trEmployeeDebits { get; set; }
    }
}
