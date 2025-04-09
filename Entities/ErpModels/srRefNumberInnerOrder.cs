using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberInnerOrder")]
    public partial class srRefNumberInnerOrder
    {
        public srRefNumberInnerOrder()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public object InnerProcessCode { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsInnerProcess bsInnerProcess { get; set; }

    }
}
