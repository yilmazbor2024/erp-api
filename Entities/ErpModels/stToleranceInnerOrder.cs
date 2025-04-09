using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stToleranceInnerOrder")]
    public partial class stToleranceInnerOrder
    {
        public stToleranceInnerOrder()
        {
        }

        [Key]
        [Required]
        public Guid InnerOrderLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
