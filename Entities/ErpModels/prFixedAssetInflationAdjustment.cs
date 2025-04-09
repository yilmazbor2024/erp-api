using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prFixedAssetInflationAdjustment")]
    public partial class prFixedAssetInflationAdjustment
    {
        public prFixedAssetInflationAdjustment()
        {
        }

        [Key]
        [Required]
        public Guid FixedAssetExpenseID { get; set; }

        [Required]
        public decimal DiffDepreciationInflationAdjustment { get; set; }

        [Required]
        public decimal DiffCostOfFixedAssetInflationAdjustment { get; set; }

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
        public virtual prFixedAssetExpense prFixedAssetExpense { get; set; }

    }
}
