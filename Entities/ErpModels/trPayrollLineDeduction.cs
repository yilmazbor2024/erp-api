using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trPayrollLineDeduction")]
    public partial class trPayrollLineDeduction
    {
        public trPayrollLineDeduction()
        {
        }

        [Key]
        [Required]
        public Guid PayrollLineDeductionID { get; set; }

        [Required]
        public Guid PayrollLineID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeductionCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeductionCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal AmountAdded { get; set; }

        public Guid? EmployeeDebitID { get; set; }

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
        public virtual cdDeduction cdDeduction { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
