using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCostOfGoodsSoldHeader")]
    public partial class trCostOfGoodsSoldHeader
    {
        public trCostOfGoodsSoldHeader()
        {
            trAdjustCostInventorys = new HashSet<trAdjustCostInventory>();
            trCostOfGoodsSoldLines = new HashSet<trCostOfGoodsSoldLine>();
            trEndOfPeriodInventorys = new HashSet<trEndOfPeriodInventory>();
        }

        [Key]
        [Required]
        public Guid CostOfGoodsSoldHeaderID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CostOfGoodsSoldPeriodCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OfficeCOGSGrCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

        [Required]
        public byte CostingMethodCode { get; set; }

        [Required]
        public byte CostingLevelCode { get; set; }

        [Required]
        public byte CostingVariantLevelCode { get; set; }

        [Required]
        public bool CalculateByBatchCode { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public bool IsInflationAdjustment { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

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
        public virtual bsCostingVariantLevel bsCostingVariantLevel { get; set; }
        public virtual cdBatch cdBatch { get; set; }
        public virtual bsCostingMethod bsCostingMethod { get; set; }
        public virtual cdCostOfGoodsSoldPeriod cdCostOfGoodsSoldPeriod { get; set; }
        public virtual bsCostingLevel bsCostingLevel { get; set; }

        public virtual ICollection<trAdjustCostInventory> trAdjustCostInventorys { get; set; }
        public virtual ICollection<trCostOfGoodsSoldLine> trCostOfGoodsSoldLines { get; set; }
        public virtual ICollection<trEndOfPeriodInventory> trEndOfPeriodInventorys { get; set; }
    }
}
