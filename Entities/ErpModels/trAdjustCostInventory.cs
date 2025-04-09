using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAdjustCostInventory")]
    public partial class trAdjustCostInventory
    {
        public trAdjustCostInventory()
        {
            trAdjustCostInventoryLines = new HashSet<trAdjustCostInventoryLine>();
        }

        [Key]
        [Required]
        public Guid AdjustCostHeaderID { get; set; }

        [Key]
        [Required]
        public Guid CostOfGoodsSoldHeaderID { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

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
        public virtual trAdjustCostHeader trAdjustCostHeader { get; set; }
        public virtual trCostOfGoodsSoldHeader trCostOfGoodsSoldHeader { get; set; }

        public virtual ICollection<trAdjustCostInventoryLine> trAdjustCostInventoryLines { get; set; }
    }
}
