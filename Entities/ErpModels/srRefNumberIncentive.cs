using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberIncentive")]
    public partial class srRefNumberIncentive
    {
        public srRefNumberIncentive()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public decimal OfficeCodeNumber { get; set; }

        [Key]
        [Required]
        public decimal StoreCodeNumber { get; set; }

        [Required]
        public decimal IncentiveNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

    }
}
