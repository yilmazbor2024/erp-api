using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trExpenseAccrualInflationAdjustmentLine")]
    public partial class trExpenseAccrualInflationAdjustmentLine
    {
        public trExpenseAccrualInflationAdjustmentLine()
        {
        }

        [Key]
        [Required]
        public Guid ExpenseAccrualLineID { get; set; }

        [Key]
        [Required]
        public int AdjustmentYear { get; set; }

        [Key]
        [Required]
        public byte AdjustmentMonth { get; set; }

        [Required]
        public double AdjustmentPPIRate { get; set; }

        [Required]
        public double AdjustmentRate { get; set; }

        [Required]
        public decimal AdjustmentAmount { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        public Guid? JournalHeaderID { get; set; }

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
        public virtual trExpenseAccrualLine trExpenseAccrualLine { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }

    }
}
