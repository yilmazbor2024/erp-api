using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("srRefNumberBankTrans")]
    public partial class srRefNumberBankTrans
    {
        public srRefNumberBankTrans()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte BankTransTypeCode { get; set; }

        [Required]
        public decimal BankTransNumber { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsBankTransType bsBankTransType { get; set; }

    }
}
