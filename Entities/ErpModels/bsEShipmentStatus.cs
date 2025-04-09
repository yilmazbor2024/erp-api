using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEShipmentStatus")]
    public partial class bsEShipmentStatus
    {
        public bsEShipmentStatus()
        {
            bsEShipmentStatusDescs = new HashSet<bsEShipmentStatusDesc>();
            tpInnerHeaderExtensions = new HashSet<tpInnerHeaderExtension>();
            tpShipmentHeaderExtensions = new HashSet<tpShipmentHeaderExtension>();
        }

        [Key]
        [Required]
        public byte EShipmentStatusCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsEShipmentStatusDesc> bsEShipmentStatusDescs { get; set; }
        public virtual ICollection<tpInnerHeaderExtension> tpInnerHeaderExtensions { get; set; }
        public virtual ICollection<tpShipmentHeaderExtension> tpShipmentHeaderExtensions { get; set; }
    }
}
