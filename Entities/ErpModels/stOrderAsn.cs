using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("stOrderAsn")]
    public partial class stOrderAsn
    {
        public stOrderAsn()
        {
        }

        [Key]
        [Required]
        public Guid OrderLineID { get; set; }

        [Key]
        [Required]
        public Guid OrderAsnLineID { get; set; }

        [Required]
        public double Qty1 { get; set; }

    }
}
