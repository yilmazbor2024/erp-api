using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDispOrderType")]
    public partial class bsDispOrderType
    {
        public bsDispOrderType()
        {
            bsDispOrderTypeDescs = new HashSet<bsDispOrderTypeDesc>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
        }

        [Key]
        [Required]
        public byte DispOrderTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDispOrderTypeDesc> bsDispOrderTypeDescs { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
    }
}
