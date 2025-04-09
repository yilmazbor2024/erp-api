using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPointRecordType")]
    public partial class bsPointRecordType
    {
        public bsPointRecordType()
        {
            bsPointRecordTypeDescs = new HashSet<bsPointRecordTypeDesc>();
            prDiscountPoints = new HashSet<prDiscountPoint>();
        }

        [Key]
        [Required]
        public byte PointRecordTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPointRecordTypeDesc> bsPointRecordTypeDescs { get; set; }
        public virtual ICollection<prDiscountPoint> prDiscountPoints { get; set; }
    }
}
