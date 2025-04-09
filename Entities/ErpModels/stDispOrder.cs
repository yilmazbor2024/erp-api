using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stDispOrder")]
    public partial class stDispOrder
    {
        public stDispOrder()
        {
        }

        [Required]
        public Guid OrderLineID { get; set; }

        public Guid? ReserveLineID { get; set; }

        [Key]
        [Required]
        public Guid DispOrderLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

        [Required]
        public double FcQty1 { get; set; }

    }
}
