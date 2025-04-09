using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsOrderType")]
    public partial class bsOrderType
    {
        public bsOrderType()
        {
            bsOrderTypeDescs = new HashSet<bsOrderTypeDesc>();
            trOrderHeaders = new HashSet<trOrderHeader>();
        }

        [Key]
        [Required]
        public byte OrderTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsOrderTypeDesc> bsOrderTypeDescs { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
    }
}
