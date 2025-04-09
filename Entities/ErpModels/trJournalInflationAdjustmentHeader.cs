using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trJournalInflationAdjustmentHeader")]
    public partial class trJournalInflationAdjustmentHeader
    {
        public trJournalInflationAdjustmentHeader()
        {
            trJournalInflationAdjustmentLines = new HashSet<trJournalInflationAdjustmentLine>();
        }

        [Key]
        [Required]
        public Guid JournalInflationAdjustmentHeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [Required]
        public int AdjustmentYear { get; set; }

        [Required]
        public byte AdjustmentMonth { get; set; }

        [Required]
        public double AdjustmentPPIRate { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }

        public virtual ICollection<trJournalInflationAdjustmentLine> trJournalInflationAdjustmentLines { get; set; }
    }
}
