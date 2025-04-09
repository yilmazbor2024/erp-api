using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfPayrollDefault")]
    public partial class dfPayrollDefault
    {
        public dfPayrollDefault()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public short ValidYear { get; set; }

        [Key]
        [Required]
        public byte ValidMonth { get; set; }

        [Required]
        public short SGKMonthlyDayCount { get; set; }

        [Required]
        public float ShiftHour { get; set; }

        [Required]
        public byte MaxLeaveHourPerMonth { get; set; }

        [Required]
        public float StampDutyRate { get; set; }

        [Required]
        public decimal SeverancePayCeiling { get; set; }

        [Required]
        public int RoundingDigit { get; set; }

        [Required]
        public bool CalculateMissingHoursAsDeduction { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MissingHoursDeductionCode { get; set; }

        [Required]
        public bool CloseAccrualMonthWithJobDepartmentDetail { get; set; }

        [Required]
        public float OvertimeRate1 { get; set; }

        [Required]
        public float OvertimeRate2 { get; set; }

        [Required]
        public float OvertimeRate3 { get; set; }

        [Required]
        public float MaxOvertimeHour { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string MaxOvertimeEarningCode { get; set; }

        [Required]
        public decimal SGKDailyBaseMin { get; set; }

        [Required]
        public decimal SGKDailyBaseMax { get; set; }

        [Required]
        public decimal SGKDailyBaseMinSmallThan16 { get; set; }

        [Required]
        public decimal SGKDailyBaseMaxSmallThan16 { get; set; }

        [Required]
        public bool CalcBeforeTaxForSGKBaseTransferred { get; set; }

        [Required]
        public decimal MinimumWageMin { get; set; }

        [Required]
        public decimal MinimumWageMinSmallThan16 { get; set; }

        [Required]
        public decimal MinimumWageNetAmount { get; set; }

        [Required]
        public decimal MinimumWageNetAmountWithAGI { get; set; }

        [Required]
        public decimal AGIBase { get; set; }

        [Required]
        public decimal AGIBaseSmallThan16 { get; set; }

        [Required]
        public float AGIEmployeeRate { get; set; }

        [Required]
        public float AGISpouseRate { get; set; }

        [Required]
        public float AGIFirstChildRate { get; set; }

        [Required]
        public float AGISecondChildRate { get; set; }

        [Required]
        public float AGIThirdChildRate { get; set; }

        [Required]
        public float AGIUpwardsThreeChildRate { get; set; }

        [Required]
        public decimal IncomeTaxBracket1MaxAmount { get; set; }

        [Required]
        public float IncomeTaxBracket1Rate { get; set; }

        [Required]
        public decimal IncomeTaxBracket2MaxAmount { get; set; }

        [Required]
        public float IncomeTaxBracket2Rate { get; set; }

        [Required]
        public decimal IncomeTaxBracket3MaxAmount { get; set; }

        [Required]
        public float IncomeTaxBracket3Rate { get; set; }

        [Required]
        public decimal IncomeTaxBracket4MaxAmount { get; set; }

        [Required]
        public float IncomeTaxBracket4Rate { get; set; }

        [Required]
        public decimal IncomeTaxBracket5MaxAmount { get; set; }

        [Required]
        public float IncomeTaxBracket5Rate { get; set; }

        [Required]
        public bool UseCompulsoryPensionInsuranceDeduction { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CPIDeductionCode { get; set; }

        [Required]
        public float CPIDeductionRate { get; set; }

        [Required]
        public decimal MaxCPIDeductionAmount { get; set; }

        [Required]
        public bool CPINotSubjectToPrivateInsuranceRelief { get; set; }

        [Required]
        public bool IncludePrivateInsuranceBasePaidByEmployer { get; set; }

        [Required]
        public bool GarnishmentAmountCalculateByTotalGarnishmentEarnings { get; set; }

        [Required]
        public bool SeveranceBaseCanExceedLegalBase { get; set; }

        [Required]
        public bool AllowedToUseTotalSeverancePayForGarnishment { get; set; }

        [Required]
        public bool AllowedToUseTotalTerminationPayForGarnishment { get; set; }

        [Required]
        public byte MissingWorkSGKBaseTurnoverPolicy { get; set; }

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

        [Required]
        public bool CalculateCompanyPaidPIAsEarnings { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdDeduction cdDeduction { get; set; }

    }
}
