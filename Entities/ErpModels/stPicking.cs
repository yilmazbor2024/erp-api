using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stPicking")]
    public partial class stPicking
    {
        public stPicking()
        {
        }

        [Required]
        public Guid OrderLineID { get; set; }

        public Guid? ReserveLineID { get; set; }

        public Guid? DispOrderLineID { get; set; }

        [Key]
        [Required]
        public Guid PickingLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
