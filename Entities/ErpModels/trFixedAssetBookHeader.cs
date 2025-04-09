using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trFixedAssetBookHeader")]
    public partial class trFixedAssetBookHeader
    {
        public trFixedAssetBookHeader()
        {
            trFixedAssetBookLines = new HashSet<trFixedAssetBookLine>();
        }

        [Key]
        [Required]
        public Guid FixedAssetBookHeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public short ValidYear { get; set; }

        [Required]
        public byte DepreciationMethodCode { get; set; }

        [Required]
        public bool IsVUK { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocCurrencyCode { get; set; }

        [Required]
        public decimal BookValueAtBeginningOfPeriod { get; set; }

        [Required]
        public float DepreciationRate { get; set; }

        [Required]
        public float ReassessmentRate { get; set; }

        [Required]
        public decimal DepreciationExpense { get; set; }

        [Required]
        public decimal ReassessmentExpense { get; set; }

        [Required]
        public decimal ReassessmentAmount { get; set; }

        [Required]
        public decimal SalesAmount { get; set; }

        [Required]
        public decimal PeriodDepreciation { get; set; }

        [Required]
        public decimal AccumulatedDepreciation { get; set; }

        [Required]
        public decimal BookValueAtEndOfPeriod { get; set; }

        [Required]
        public decimal PeriodBookValue { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdItem cdItem { get; set; }

        public virtual ICollection<trFixedAssetBookLine> trFixedAssetBookLines { get; set; }
    }
}
