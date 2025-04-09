using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankCreditGuaranteeType")]
    public partial class bsBankCreditGuaranteeType
    {
        public bsBankCreditGuaranteeType()
        {
            bsBankCreditGuaranteeTypeDescs = new HashSet<bsBankCreditGuaranteeTypeDesc>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
        }

        [Key]
        [Required]
        public byte BankCreditGuaranteeTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBankCreditGuaranteeTypeDesc> bsBankCreditGuaranteeTypeDescs { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
    }
}
