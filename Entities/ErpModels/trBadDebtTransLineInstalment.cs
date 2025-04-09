using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtTransLineInstalment")]
    public partial class trBadDebtTransLineInstalment
    {
        public trBadDebtTransLineInstalment()
        {
        }

        [Key]
        [Required]
        public Guid BadDebtTransLineInstalmentID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Debit { get; set; }

        [Required]
        public decimal PaidDebit { get; set; }

        [Required]
        public decimal LateChargeAmount { get; set; }

        [Required]
        public decimal PaidLateChargeAmount { get; set; }

        [Required]
        public double LateChargeRate { get; set; }

        [Required]
        public decimal Expense { get; set; }

        [Required]
        public decimal PaidExpense { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public Guid? DebitLineID { get; set; }

        public Guid? OrderPaymentPlanID { get; set; }

        [Required]
        public Guid BadDebtTransLineID { get; set; }

        [Required]
        public bool DoNotAccrueExpenseAmount { get; set; }

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
        public virtual trBadDebtTransLine trBadDebtTransLine { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trDebitLine trDebitLine { get; set; }
        public virtual trOrderPaymentPlan trOrderPaymentPlan { get; set; }

    }
}
