using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trFixedAssetBookLinePeriodDepreciationExpenseDetail")]
    public partial class trFixedAssetBookLinePeriodDepreciationExpenseDetail
    {
        public trFixedAssetBookLinePeriodDepreciationExpenseDetail()
        {
        }

        [Key]
        [Required]
        public Guid FixedAssetBookLineID { get; set; }

        [Key]
        [Required]
        public Guid FixedAssetExpenseID { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
        public virtual trFixedAssetBookLine trFixedAssetBookLine { get; set; }
        public virtual prFixedAssetExpense prFixedAssetExpense { get; set; }

    }
}
