using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_InboxShipmentStatus")]
    public partial class e_InboxShipmentStatus
    {
        public e_InboxShipmentStatus()
        {
            e_InboxShipmentHeaders = new HashSet<e_InboxShipmentHeader>();
            e_InboxShipmentResponseHeaders = new HashSet<e_InboxShipmentResponseHeader>();
        }

        [Key]
        [Required]
        public byte InboxShipmentStatusCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string InboxShipmentStatusDescription { get; set; }

        public virtual ICollection<e_InboxShipmentHeader> e_InboxShipmentHeaders { get; set; }
        public virtual ICollection<e_InboxShipmentResponseHeader> e_InboxShipmentResponseHeaders { get; set; }
    }
}
