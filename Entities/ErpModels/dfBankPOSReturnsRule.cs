using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfBankPOSReturnsRule")]
    public partial class dfBankPOSReturnsRule
    {
        public dfBankPOSReturnsRule()
        {
            dfBankPOSReturnsTermRules = new HashSet<dfBankPOSReturnsTermRule>();
        }

        [Key]
        [Required]
        public Guid BankPOSReturnRuleID { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [Required]
        public bool ValidForOtherBankCards { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [Required]
        public byte InstallmentCount { get; set; }

        [Required]
        public DateTime ValidDate { get; set; }

        [Required]
        public byte SalesReturnOption { get; set; }

        [Required]
        public short ReturnDay { get; set; }

        [Required]
        public bool ServiceFeeOption { get; set; }

        [Required]
        public bool PosPointOption { get; set; }

        [Required]
        public bool SalesDateOptionSat { get; set; }

        [Required]
        public bool SalesDateOptionSun { get; set; }

        [Required]
        public bool SalesDateOptionOfficialOff { get; set; }

        [Required]
        public bool TermDateOptionSat { get; set; }

        [Required]
        public bool TermDateOptionSun { get; set; }

        [Required]
        public bool TermDateOptionOfficialOff { get; set; }

        [Required]
        public bool ValidForAllBanks { get; set; }

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
        public virtual cdBank cdBank { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCreditCardType cdCreditCardType { get; set; }

        public virtual ICollection<dfBankPOSReturnsTermRule> dfBankPOSReturnsTermRules { get; set; }
    }
}
