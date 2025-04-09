using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCostingMethod")]
    public partial class bsCostingMethod
    {
        public bsCostingMethod()
        {
            bsCostingMethodDescs = new HashSet<bsCostingMethodDesc>();
            dfCompanyCostOfGoodsSolds = new HashSet<dfCompanyCostOfGoodsSold>();
            trCostOfGoodsSoldHeaders = new HashSet<trCostOfGoodsSoldHeader>();
        }

        [Key]
        [Required]
        public byte CostingMethodCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCostingMethodDesc> bsCostingMethodDescs { get; set; }
        public virtual ICollection<dfCompanyCostOfGoodsSold> dfCompanyCostOfGoodsSolds { get; set; }
        public virtual ICollection<trCostOfGoodsSoldHeader> trCostOfGoodsSoldHeaders { get; set; }
    }
}
