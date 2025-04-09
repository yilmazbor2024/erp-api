using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("hrEmployeeMonthlySumDetail")]
    public partial class hrEmployeeMonthlySumDetail
    {
        public hrEmployeeMonthlySumDetail()
        {
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

        [Key]
        [Required]
        public short ValidYear { get; set; }

        [Key]
        [Required]
        public byte ValidMonth { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WorkPlaceCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobDepartmentCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Key]
        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal GrossIncome { get; set; }

        [Required]
        public decimal NetIncome { get; set; }

        [Required]
        public decimal DeductionAmount { get; set; }

        [Required]
        public byte PremiumDay { get; set; }

        [Required]
        public byte MissingWorkDay { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MissingWorkReasonCode { get; set; }

        [Required]
        public decimal SGKBase { get; set; }

        [Required]
        public decimal SGKBaseTransferred { get; set; }

        [Required]
        public decimal SGKBaseTransferorUsed { get; set; }

        [Required]
        public decimal UnemploymentEmployee { get; set; }

        [Required]
        public decimal UnemploymentEmployer { get; set; }

        [Required]
        public decimal UnemploymentEmployerAdd { get; set; }

        [Required]
        public decimal PensionEmployee { get; set; }

        [Required]
        public decimal PensionEmployer { get; set; }

        [Required]
        public decimal PensionEmployerAdd { get; set; }

        [Required]
        public decimal HealthEmployee { get; set; }

        [Required]
        public decimal HealthEmployer { get; set; }

        [Required]
        public decimal HealthEmployerAdd { get; set; }

        [Required]
        public decimal SGDPEmployee { get; set; }

        [Required]
        public decimal SGDPEmployer { get; set; }

        [Required]
        public decimal SGDPEmployerAdd { get; set; }

        [Required]
        public decimal ShortTermInsurance { get; set; }

        [Required]
        public decimal IncomeTaxRelief { get; set; }

        [Required]
        public decimal IncomeTaxBase { get; set; }

        [Required]
        public decimal IncomeTaxAmount { get; set; }

        [Required]
        public decimal StampDutyBase { get; set; }

        [Required]
        public decimal StampDutyAmount { get; set; }

        [Required]
        public decimal StampDutyRelief { get; set; }

        [Required]
        public decimal StampDutyReliefBase { get; set; }

        [Required]
        public decimal IncomeTaxIncentiveAmount { get; set; }

        [Required]
        public decimal MinWageTaxReliefBase { get; set; }

        [Required]
        public decimal MinWageTaxReliefAmount { get; set; }

        [Required]
        public decimal MinWageStampDutyReliefBase { get; set; }

        [Required]
        public decimal MinWageStampDutyReliefAmount { get; set; }

        [Required]
        public float AGIRate { get; set; }

        [Required]
        public decimal AGIAmount { get; set; }

        [Required]
        public decimal ELDEmployee { get; set; }

        [Required]
        public decimal ELDEmployer { get; set; }

        [Required]
        public decimal RDReliefAmount { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public bool IsUnchangeable { get; set; }

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
        public virtual cdMissingWorkReason cdMissingWorkReason { get; set; }
        public virtual hrEmployeePayrollProfile hrEmployeePayrollProfile { get; set; }
        public virtual cdEarnings cdEarnings { get; set; }
        public virtual cdJobDepartment cdJobDepartment { get; set; }

    }
}
