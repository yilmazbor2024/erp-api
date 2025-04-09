using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerLineCostCenterRates")]
    public partial class trInnerLineCostCenterRates
    {
        public trInnerLineCostCenterRates()
        {
        }

        [Key]
        [Required]
        public Guid InnerLineID { get; set; }

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
        public virtual trInnerLine trInnerLine { get; set; }
        public virtual prCostCenterHierarchy prCostCenterHierarchy { get; set; }

    }
}
