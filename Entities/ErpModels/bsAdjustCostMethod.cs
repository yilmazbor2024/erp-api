using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsAdjustCostMethod")]
    public partial class bsAdjustCostMethod
    {
        public bsAdjustCostMethod()
        {
            bsAdjustCostMethodDescs = new HashSet<bsAdjustCostMethodDesc>();
        }

        [Key]
        [Required]
        public byte AdjustCostMethodCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsAdjustCostMethodDesc> bsAdjustCostMethodDescs { get; set; }
    }
}
