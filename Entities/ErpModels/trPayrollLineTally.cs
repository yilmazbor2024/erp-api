using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPayrollLineTally")]
    public partial class trPayrollLineTally
    {
        public trPayrollLineTally()
        {
        }

        [Key]
        [Required]
        public Guid PayrollLineID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCode { get; set; }

        [Required]
        public byte WorkDay { get; set; }

        [Required]
        public double WorkHour { get; set; }

        [Required]
        public byte WeekendDay { get; set; }

        [Required]
        public double WeekendHour { get; set; }

        [Required]
        public byte OfficialOffDay { get; set; }

        [Required]
        public double officialOffHour { get; set; }

        [Required]
        public byte ReligiousHoliday { get; set; }

        [Required]
        public double ReligiousHolidayHour { get; set; }

        [Required]
        public byte SabbaticalDay { get; set; }

        [Required]
        public double SabbaticalHour { get; set; }

        [Required]
        public double MissingHours { get; set; }

        [Required]
        public double RDHours { get; set; }

        [Required]
        public double OvertimeHour { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public double EarningsDay { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Amount2 { get; set; }

        [Required]
        public decimal Amount3 { get; set; }

        [Required]
        public bool IsGrossEarnings { get; set; }

        [Required]
        public decimal EarningsWorkDay { get; set; }

        [Required]
        public decimal EarningsWeekendDay { get; set; }

        [Required]
        public decimal EarningsOfficialOffDay { get; set; }

        [Required]
        public decimal EarningsReligiousHoliday { get; set; }

        [Required]
        public decimal EarningsSabbaticalDay { get; set; }

        [Required]
        public decimal EarningsOther { get; set; }

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
        public decimal MinWageTaxReliefBase { get; set; }

        [Required]
        public decimal MinWageTaxReliefAmount { get; set; }

        [Required]
        public decimal MinWageStampDutyReliefBase { get; set; }

        [Required]
        public decimal MinWageStampDutyReliefAmount { get; set; }

        [Required]
        public decimal NonTaxableIncomeTaxBase { get; set; }

        [Required]
        public decimal NonTaxableStampDutyBase { get; set; }

        [Required]
        public decimal SGKRelief { get; set; }

        [Required]
        public decimal SGKBase { get; set; }

        [Required]
        public decimal UnemploymentEmployee { get; set; }

        [Required]
        public decimal PensionEmployee { get; set; }

        [Required]
        public decimal HealthEmployee { get; set; }

        [Required]
        public decimal SGDPEmployee { get; set; }

        [Required]
        public decimal UnemploymentEmployer { get; set; }

        [Required]
        public decimal PensionEmployer { get; set; }

        [Required]
        public decimal HealthEmployer { get; set; }

        [Required]
        public decimal SGDPEmployer { get; set; }

        [Required]
        public decimal ShortTermInsurance { get; set; }

        [Required]
        public decimal WageGarnishment { get; set; }

        [Required]
        public decimal RDReliefAmount { get; set; }

        [Required]
        public decimal AGIAmount { get; set; }

        [Required]
        public decimal NetAmount { get; set; }

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
        public virtual trPayrollLine trPayrollLine { get; set; }
        public virtual cdEarnings cdEarnings { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
