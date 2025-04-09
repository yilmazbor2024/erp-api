using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prFixedAssetDepreciationInfo")]
    public partial class prFixedAssetDepreciationInfo
    {
        public prFixedAssetDepreciationInfo()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public byte FixedAssetTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocCurrencyCode { get; set; }

        [Required]
        public decimal CostOfFixedAsset { get; set; }

        [Required]
        public DateTime DepreciationBeginningDate { get; set; }

        [Required]
        public decimal SalvageValue { get; set; }

        [Required]
        public decimal PurchaseAmountOfFixedAsset { get; set; }

        [Required]
        public double Qty { get; set; }

        [Required]
        public float DepreciationRate { get; set; }

        [Required]
        public byte UsefulLifeOfAsset { get; set; }

        [Required]
        public int UsefulLifeOfAssetByDay { get; set; }

        [Required]
        public byte DepreciationMethodCode { get; set; }

        [Required]
        public bool HaveReassessment { get; set; }

        [Required]
        public bool HaveReassessmentDepreciation { get; set; }

        [Required]
        public bool HaveProrataDepreciation { get; set; }

        [Required]
        public decimal Vat { get; set; }

        [Required]
        public byte VatTerm { get; set; }

        [Required]
        public DateTime TurnOverDate { get; set; }

        [Required]
        public decimal AccumulatedDepreciation { get; set; }

        [Required]
        public decimal BookValueAtEndOfYear { get; set; }

        [Required]
        public decimal AccumulatedDepreciationTMS16 { get; set; }

        [Required]
        public decimal BookValueAtEndOfYearTMS16 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public float DepreciationRateTMS16 { get; set; }

        [Required]
        public byte UsefulLifeOfAssetTMS16 { get; set; }

        [Required]
        public byte DepreciationMethodCodeTMS16 { get; set; }

        [Required]
        public bool HaveNoDepreciation { get; set; }

        [Required]
        public DateTime LastReassessmentDate { get; set; }

        [Required]
        public bool IsDailyDepreciation { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdFixedAssetType cdFixedAssetType { get; set; }
        public virtual bsDepreciationMethod bsDepreciationMethod { get; set; }
        public virtual cdItem cdItem { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }

    }
}
