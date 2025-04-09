using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountVoucherBase")]
    public partial class bsDiscountVoucherBase
    {
        public bsDiscountVoucherBase()
        {
            bsDiscountVoucherBaseDescs = new HashSet<bsDiscountVoucherBaseDesc>();
            cdDiscountVoucherTypes = new HashSet<cdDiscountVoucherType>();
        }

        [Key]
        [Required]
        public byte DiscountVoucherBaseCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDiscountVoucherBaseDesc> bsDiscountVoucherBaseDescs { get; set; }
        public virtual ICollection<cdDiscountVoucherType> cdDiscountVoucherTypes { get; set; }
    }
}
