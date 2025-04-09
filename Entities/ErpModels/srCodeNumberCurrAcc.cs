using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srCodeNumberCurrAcc")]
    public partial class srCodeNumberCurrAcc
    {
        public srCodeNumberCurrAcc()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Required]
        public decimal OfficeCodeNumber { get; set; }

        [Key]
        [Required]
        public decimal StoreCodeNumber { get; set; }

        [Required]
        public decimal LastNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsCurrAccType bsCurrAccType { get; set; }

    }
}
