using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCostingVariantLevel")]
    public partial class bsCostingVariantLevel
    {
        public bsCostingVariantLevel()
        {
            bsCostingVariantLevelDescs = new HashSet<bsCostingVariantLevelDesc>();
            dfCompanyCostOfGoodsSolds = new HashSet<dfCompanyCostOfGoodsSold>();
            trCostOfGoodsSoldHeaders = new HashSet<trCostOfGoodsSoldHeader>();
        }

        [Key]
        [Required]
        public byte CostingVariantLevelCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCostingVariantLevelDesc> bsCostingVariantLevelDescs { get; set; }
        public virtual ICollection<dfCompanyCostOfGoodsSold> dfCompanyCostOfGoodsSolds { get; set; }
        public virtual ICollection<trCostOfGoodsSoldHeader> trCostOfGoodsSoldHeaders { get; set; }
    }
}
