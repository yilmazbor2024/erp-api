using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stInnerOrder")]
    public partial class stInnerOrder
    {
        public stInnerOrder()
        {
        }

        [Key]
        [Required]
        public Guid InnerOrderLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
