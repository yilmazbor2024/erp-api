using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prFixedAssetExpenseReassessment")]
    public partial class prFixedAssetExpenseReassessment
    {
        public prFixedAssetExpenseReassessment()
        {
        }

        [Key]
        [Required]
        public Guid FixedAssetExpenseID { get; set; }

        [Required]
        public decimal DiffAccumulatedDepreciation { get; set; }

        [Required]
        public decimal DiffCostOfFixedAsset { get; set; }

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
