using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberBankPaymentList")]
    public partial class srRefNumberBankPaymentList
    {
        public srRefNumberBankPaymentList()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public decimal BankPaymentListNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }

    }
}
