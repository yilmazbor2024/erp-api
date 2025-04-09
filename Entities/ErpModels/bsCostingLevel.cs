using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCostingLevel")]
    public partial class bsCostingLevel
    {
        public bsCostingLevel()
        {
            bsCostingLevelDescs = new HashSet<bsCostingLevelDesc>();
            dfCompanyCostOfGoodsSolds = new HashSet<dfCompanyCostOfGoodsSold>();
            trCostOfGoodsSoldHeaders = new HashSet<trCostOfGoodsSoldHeader>();
        }

        [Key]
        [Required]
        public byte CostingLevelCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCostingLevelDesc> bsCostingLevelDescs { get; set; }
        public virtual ICollection<dfCompanyCostOfGoodsSold> dfCompanyCostOfGoodsSolds { get; set; }
        public virtual ICollection<trCostOfGoodsSoldHeader> trCostOfGoodsSoldHeaders { get; set; }
    }
}
