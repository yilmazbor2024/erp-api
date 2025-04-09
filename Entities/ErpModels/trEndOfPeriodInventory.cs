using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trEndOfPeriodInventory")]
    public partial class trEndOfPeriodInventory
    {
        public trEndOfPeriodInventory()
        {
            trAdjustCostInventoryLines = new HashSet<trAdjustCostInventoryLine>();
        }

        [Key]
        [Required]
        public Guid EndOfPeriodInventoryID { get; set; }

        public DateTime? OperationDate { get; set; }

        public TimeSpan? OperationTime { get; set; }

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
        public double Qty1 { get; set; }

        [Required]
        public double Qty2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal CostAmount { get; set; }

        [Required]
        public decimal CostPriceWithInflation { get; set; }

        [Required]
        public decimal CostAmountWithInflation { get; set; }

        [Required]
        public Guid CostOfGoodsSoldHeaderID { get; set; }

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
        public virtual trCostOfGoodsSoldHeader trCostOfGoodsSoldHeader { get; set; }

        public virtual ICollection<trAdjustCostInventoryLine> trAdjustCostInventoryLines { get; set; }
    }
}
