using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("e_OutboxShipmentOrder")]
    public partial class e_OutboxShipmentOrder
    {
        public e_OutboxShipmentOrder()
        {
        }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Number { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        // Navigation Properties
        public virtual e_OutboxShipmentHeader e_OutboxShipmentHeader { get; set; }

    }
}
