using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsShipmentType")]
    public partial class bsShipmentType
    {
        public bsShipmentType()
        {
            bsShipmentTypeDescs = new HashSet<bsShipmentTypeDesc>();
            tpInnerHeaderExtensions = new HashSet<tpInnerHeaderExtension>();
            tpShipmentHeaderExtensions = new HashSet<tpShipmentHeaderExtension>();
        }

        [Key]
        [Required]
        public byte ShipmentTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsShipmentTypeDesc> bsShipmentTypeDescs { get; set; }
        public virtual ICollection<tpInnerHeaderExtension> tpInnerHeaderExtensions { get; set; }
        public virtual ICollection<tpShipmentHeaderExtension> tpShipmentHeaderExtensions { get; set; }
    }
}
