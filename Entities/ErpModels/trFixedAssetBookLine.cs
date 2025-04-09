using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trFixedAssetBookLine")]
    public partial class trFixedAssetBookLine
    {
        public trFixedAssetBookLine()
        {
            trFixedAssetBookLinePeriodDepreciationExpenseDetails = new HashSet<trFixedAssetBookLinePeriodDepreciationExpenseDetail>();
        }

        [Key]
        [Required]
        public Guid FixedAssetBookLineID { get; set; }

        [Required]
        public short ValidYear { get; set; }

        [Required]
        public byte ValidMonth { get; set; }

        [Required]
        public decimal PeriodDepreciation { get; set; }

        [Required]
        public decimal periodDepreciationExpense { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        public Guid? JournalHeaderID { get; set; }

        [Required]
        public Guid FixedAssetBookHeaderID { get; set; }

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
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual trFixedAssetBookHeader trFixedAssetBookHeader { get; set; }

        public virtual ICollection<trFixedAssetBookLinePeriodDepreciationExpenseDetail> trFixedAssetBookLinePeriodDepreciationExpenseDetails { get; set; }
    }
}
