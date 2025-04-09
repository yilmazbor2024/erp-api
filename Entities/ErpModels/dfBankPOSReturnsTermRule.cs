using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfBankPOSReturnsTermRule")]
    public partial class dfBankPOSReturnsTermRule
    {
        public dfBankPOSReturnsTermRule()
        {
        }

        [Key]
        [Required]
        public Guid BankPOSReturnRuleID { get; set; }

        [Key]
        [Required]
        public byte InstallmentNumber { get; set; }

        [Required]
        public float ServiceFeeRate { get; set; }

        [Required]
        public float TaxRate { get; set; }

        [Required]
        public float PosPointRate { get; set; }

        [Required]
        public short TermDay { get; set; }

        [Required]
        public float EarlyPaymentDiscountRate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        // Navigation Properties
        public virtual dfBankPOSReturnsRule dfBankPOSReturnsRule { get; set; }

    }
}
