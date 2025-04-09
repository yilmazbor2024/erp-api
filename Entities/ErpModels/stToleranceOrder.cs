using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stToleranceOrder")]
    public partial class stToleranceOrder
    {
        public stToleranceOrder()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
