using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberOtherPayment")]
    public partial class srRefNumberOtherPayment
    {
        public srRefNumberOtherPayment()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public decimal OtherPaymentNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

    }
}
