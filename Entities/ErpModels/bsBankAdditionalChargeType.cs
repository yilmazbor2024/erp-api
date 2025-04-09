using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankAdditionalChargeType")]
    public partial class bsBankAdditionalChargeType
    {
        public bsBankAdditionalChargeType()
        {
            bsBankAdditionalChargeTypeDescs = new HashSet<bsBankAdditionalChargeTypeDesc>();
            prBankAdditionalChargeTypeGLAccss = new HashSet<prBankAdditionalChargeTypeGLAccs>();
            trBankLineAdditionalCharges = new HashSet<trBankLineAdditionalCharge>();
        }

        [Key]
        [Required]
        public byte BankAdditionalChargeTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsBankAdditionalChargeTypeDesc> bsBankAdditionalChargeTypeDescs { get; set; }
        public virtual ICollection<prBankAdditionalChargeTypeGLAccs> prBankAdditionalChargeTypeGLAccss { get; set; }
        public virtual ICollection<trBankLineAdditionalCharge> trBankLineAdditionalCharges { get; set; }
    }
}
