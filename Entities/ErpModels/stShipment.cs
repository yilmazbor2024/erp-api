using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stShipment")]
    public partial class stShipment
    {
        public stShipment()
        {
        }

        public Guid? OrderLineID { get; set; }

        public Guid? ReserveLineID { get; set; }

        public Guid? DispOrderLineID { get; set; }

        public Guid? PickingLineID { get; set; }

        public Guid? OrderAsnLineID { get; set; }

        [Key]
        [Required]
        public Guid ShipmentLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
