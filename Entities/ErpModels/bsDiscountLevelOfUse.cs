using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountLevelOfUse")]
    public partial class bsDiscountLevelOfUse
    {
        public bsDiscountLevelOfUse()
        {
            bsDiscountLevelOfUseDescs = new HashSet<bsDiscountLevelOfUseDesc>();
            cdDiscountPointTypes = new HashSet<cdDiscountPointType>();
            cdDiscountVoucherTypes = new HashSet<cdDiscountVoucherType>();
        }

        [Key]
        [Required]
        public byte DiscountLevelOfUseCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDiscountLevelOfUseDesc> bsDiscountLevelOfUseDescs { get; set; }
        public virtual ICollection<cdDiscountPointType> cdDiscountPointTypes { get; set; }
        public virtual ICollection<cdDiscountVoucherType> cdDiscountVoucherTypes { get; set; }
    }
}
