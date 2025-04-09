using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPayrollTerminationSeveranceDetail")]
    public partial class trPayrollTerminationSeveranceDetail
    {
        public trPayrollTerminationSeveranceDetail()
        {
        }

        [Key]
        [Required]
        public Guid PayrollLineID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string EarningsCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal TotalGrossEarning { get; set; }

        [Required]
        public decimal DailyGrossIncome { get; set; }

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
        public virtual cdEarnings cdEarnings { get; set; }
        public virtual trPayrollLine trPayrollLine { get; set; }

    }
}
