using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberChequeTrans")]
    public partial class srRefNumberChequeTrans
    {
        public srRefNumberChequeTrans()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ChequeTransTypeCode { get; set; }

        [Required]
        public decimal ChequeTransNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsChequeTransType bsChequeTransType { get; set; }

    }
}
