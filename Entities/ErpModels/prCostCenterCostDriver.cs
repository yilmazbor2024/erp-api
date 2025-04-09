using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCostCenterCostDriver")]
    public partial class prCostCenterCostDriver
    {
        public prCostCenterCostDriver()
        {
        }

        [Key]
        [Required]
        public int CostCenterHierarchyID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExpensePeriodCode { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdExpensePeriod cdExpensePeriod { get; set; }
        public virtual prCostCenterHierarchy prCostCenterHierarchy { get; set; }

    }
}
