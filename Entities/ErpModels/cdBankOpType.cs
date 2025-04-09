using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBankOpType")]
    public partial class cdBankOpType
    {
        public cdBankOpType()
        {
            cdBankOpTypeDescs = new HashSet<cdBankOpTypeDesc>();
            prCurrAccOnlineBanks = new HashSet<prCurrAccOnlineBank>();
            prGLAccOnlineBanks = new HashSet<prGLAccOnlineBank>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            prOnlineBankWebServiceBankInternalParameters = new HashSet<prOnlineBankWebServiceBankInternalParameter>();
            prSubCurrAccOnlineBanks = new HashSet<prSubCurrAccOnlineBank>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
            trBankLines = new HashSet<trBankLine>();
            zpOnlineBankTransactions = new HashSet<zpOnlineBankTransaction>();
        }

        [Key]
        [Required]
        public byte BankOpTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdBankOpTypeDesc> cdBankOpTypeDescs { get; set; }
        public virtual ICollection<prCurrAccOnlineBank> prCurrAccOnlineBanks { get; set; }
        public virtual ICollection<prGLAccOnlineBank> prGLAccOnlineBanks { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<prOnlineBankWebServiceBankInternalParameter> prOnlineBankWebServiceBankInternalParameters { get; set; }
        public virtual ICollection<prSubCurrAccOnlineBank> prSubCurrAccOnlineBanks { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<zpOnlineBankTransaction> zpOnlineBankTransactions { get; set; }
    }
}
