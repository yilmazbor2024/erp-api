using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trJournalInflationAdjustmentLine")]
    public partial class trJournalInflationAdjustmentLine
    {
        public trJournalInflationAdjustmentLine()
        {
        }

        [Key]
        [Required]
        public Guid JournalInflationAdjustmentLineID { get; set; }

        [Required]
        public int ValidYear { get; set; }

        [Required]
        public byte ValidMonth { get; set; }

        [Required]
        public double PPIRate { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public decimal DebitBalance { get; set; }

        [Required]
        public decimal CreditBalance { get; set; }

        [Required]
        public decimal AdjustmentAmount { get; set; }

        [Required]
        public Guid JournalInflationAdjustmentHeaderID { get; set; }

        public Guid? JournalLineID { get; set; }

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
        public virtual trJournalInflationAdjustmentHeader trJournalInflationAdjustmentHeader { get; set; }
        public virtual trJournalLine trJournalLine { get; set; }

    }
}
