using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stOrder")]
    public partial class stOrder
    {
        public stOrder()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
