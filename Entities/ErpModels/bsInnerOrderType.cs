using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsInnerOrderType")]
    public partial class bsInnerOrderType
    {
        public bsInnerOrderType()
        {
            bsInnerOrderTypeDescs = new HashSet<bsInnerOrderTypeDesc>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
        }

        [Key]
        [Required]
        public byte InnerOrderTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsInnerOrderTypeDesc> bsInnerOrderTypeDescs { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
    }
}
