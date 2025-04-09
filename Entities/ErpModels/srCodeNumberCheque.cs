using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srCodeNumberCheque")]
    public partial class srCodeNumberCheque
    {
        public srCodeNumberCheque()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ChequeTypeCode { get; set; }

        [Key]
        [Required]
        public decimal OfficeCodeNumber { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsChequeType bsChequeType { get; set; }

    }
}
