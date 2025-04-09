using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prFixedAssetExpense")]
    public partial class prFixedAssetExpense
    {
        public prFixedAssetExpense()
        {
            prFixedAssetExpenseReassessments = new HashSet<prFixedAssetExpenseReassessment>();
            prFixedAssetInflationAdjustments = new HashSet<prFixedAssetInflationAdjustment>();
            trFixedAssetBookLinePeriodDepreciationExpenseDetails = new HashSet<trFixedAssetBookLinePeriodDepreciationExpenseDetail>();
        }

        [Key]
        [Required]
        public Guid FixedAssetExpenseID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool AddReassessment { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public double ReassessmentRate { get; set; }

        [Required]
        public short ReassessmentValidYear { get; set; }

        [Required]
        public byte ReassessmentValidMonth { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public bool AddReassessmentForTMS16 { get; set; }

        [Required]
        public bool IsInflationAdjustment { get; set; }

        [Required]
        public bool IsROFM { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal ROFMAmount { get; set; }

        public Guid? JournalHeaderID { get; set; }

        public Guid? ReassessmentRateLineID { get; set; }

        public Guid? InvoiceLineID { get; set; }

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
        public virtual cdItem cdItem { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trInvoiceLine trInvoiceLine { get; set; }
        public virtual prFixedAssetReassessmentRates prFixedAssetReassessmentRates { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }

        public virtual ICollection<prFixedAssetExpenseReassessment> prFixedAssetExpenseReassessments { get; set; }
        public virtual ICollection<prFixedAssetInflationAdjustment> prFixedAssetInflationAdjustments { get; set; }
        public virtual ICollection<trFixedAssetBookLinePeriodDepreciationExpenseDetail> trFixedAssetBookLinePeriodDepreciationExpenseDetails { get; set; }
    }
}
