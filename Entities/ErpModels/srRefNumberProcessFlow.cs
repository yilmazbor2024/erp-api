using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberProcessFlow")]
    public partial class srRefNumberProcessFlow
    {
        public srRefNumberProcessFlow()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [Required]
        public byte ProcessFlowCode { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsProcessFlow bsProcessFlow { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }

    }
}
