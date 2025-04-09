using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAdjustCostInventoryLine")]
    public partial class trAdjustCostInventoryLine
    {
        public trAdjustCostInventoryLine()
        {
        }

        [Key]
        [Required]
        public Guid AdjustCostHeaderID { get; set; }

        [Key]
        [Required]
        public Guid CostOfGoodsSoldHeaderID { get; set; }

        [Key]
        [Required]
        public Guid EndOfPeriodInventoryID { get; set; }

        [Required]
        public decimal IncreaseAmount { get; set; }

        [Required]
        public decimal ReduceAmount { get; set; }

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
        public virtual trAdjustCostInventory trAdjustCostInventory { get; set; }
        public virtual trEndOfPeriodInventory trEndOfPeriodInventory { get; set; }

    }
}
