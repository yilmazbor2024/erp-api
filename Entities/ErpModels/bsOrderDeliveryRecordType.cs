using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsOrderDeliveryRecordType")]
    public partial class bsOrderDeliveryRecordType
    {
        public bsOrderDeliveryRecordType()
        {
            bsOrderDeliveryRecordTypeDescs = new HashSet<bsOrderDeliveryRecordTypeDesc>();
            tpOrderDeliveryDetails = new HashSet<tpOrderDeliveryDetail>();
        }

        [Key]
        [Required]
        public byte OrderDeliveryRecordTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsOrderDeliveryRecordTypeDesc> bsOrderDeliveryRecordTypeDescs { get; set; }
        public virtual ICollection<tpOrderDeliveryDetail> tpOrderDeliveryDetails { get; set; }
    }
}
