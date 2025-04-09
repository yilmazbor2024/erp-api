using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTransportMode")]
    public partial class bsTransportMode
    {
        public bsTransportMode()
        {
            bsTransportModeDescs = new HashSet<bsTransportModeDesc>();
            cdShipmentMethods = new HashSet<cdShipmentMethod>();
        }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransportModeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsTransportModeDesc> bsTransportModeDescs { get; set; }
        public virtual ICollection<cdShipmentMethod> cdShipmentMethods { get; set; }
    }
}
