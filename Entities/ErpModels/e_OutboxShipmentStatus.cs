using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_OutboxShipmentStatus")]
    public partial class e_OutboxShipmentStatus
    {
        public e_OutboxShipmentStatus()
        {
            e_OutboxShipmentHeaders = new HashSet<e_OutboxShipmentHeader>();
            e_OutboxShipmentResponseHeaders = new HashSet<e_OutboxShipmentResponseHeader>();
        }

        [Key]
        [Required]
        public byte OutboxShipmentStatusCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OutboxShipmentStatusDescription { get; set; }

        public virtual ICollection<e_OutboxShipmentHeader> e_OutboxShipmentHeaders { get; set; }
        public virtual ICollection<e_OutboxShipmentResponseHeader> e_OutboxShipmentResponseHeaders { get; set; }
    }
}
