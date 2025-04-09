using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trExpenseAccrualLineCostCenterRates")]
    public partial class trExpenseAccrualLineCostCenterRates
    {
        public trExpenseAccrualLineCostCenterRates()
        {
        }

        [Key]
        [Required]
        public Guid ExpenseAccrualLineID { get; set; }

        [Key]
        [Required]
        public int CostCenterHierarchyID { get; set; }

        [Required]
        public float CostDriverRate { get; set; }

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
        public virtual prCostCenterHierarchy prCostCenterHierarchy { get; set; }
        public virtual trExpenseAccrualLine trExpenseAccrualLine { get; set; }

    }
}
