using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPointBase")]
    public partial class bsPointBase
    {
        public bsPointBase()
        {
            bsPointBaseDescs = new HashSet<bsPointBaseDesc>();
            cdDiscountPointTypes = new HashSet<cdDiscountPointType>();
        }

        [Key]
        [Required]
        public byte PointBaseCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPointBaseDesc> bsPointBaseDescs { get; set; }
        public virtual ICollection<cdDiscountPointType> cdDiscountPointTypes { get; set; }
    }
}
