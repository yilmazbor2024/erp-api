using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemCostCenterRates")]
    public partial class prItemCostCenterRates
    {
        public prItemCostCenterRates()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ExpensePeriodCode { get; set; }

        [Key]
        [Required]
        public int CostCenterHierarchyID { get; set; }

        [Required]
        public float CostDriverRate { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

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
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdItem cdItem { get; set; }
        public virtual cdExpensePeriod cdExpensePeriod { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual prCostCenterHierarchy prCostCenterHierarchy { get; set; }

    }
}
