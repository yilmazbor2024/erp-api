using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stReserve")]
    public partial class stReserve
    {
        public stReserve()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

        [Key]
        [Required]
        public Guid ReserveLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double FcQty1 { get; set; }

    }
}
