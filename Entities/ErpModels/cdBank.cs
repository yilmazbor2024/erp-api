using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBank")]
    public partial class cdBank
    {
        public cdBank()
        {
            cdBankDescs = new HashSet<cdBankDesc>();
            cdCreditCardTypes = new HashSet<cdCreditCardType>();
            dfBankPOSReturnsRules = new HashSet<dfBankPOSReturnsRule>();
            dfPaynetCompanys = new HashSet<dfPaynetCompany>();
            prBankBranchs = new HashSet<prBankBranch>();
            prBankPOSAccountss = new HashSet<prBankPOSAccounts>();
            prBankPosIDs = new HashSet<prBankPosID>();
            prBankPOSProviderConverts = new HashSet<prBankPOSProviderConvert>();
            prCreditCardTypeGLAccss = new HashSet<prCreditCardTypeGLAccs>();
            prCreditCardValiditys = new HashSet<prCreditCardValidity>();
            prCustomerDBSAccounts = new HashSet<prCustomerDBSAccount>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            prOnlineDBSLimits = new HashSet<prOnlineDBSLimit>();
            prOnlineDBSLimitHistorys = new HashSet<prOnlineDBSLimitHistory>();
            prUniFreeTenderTypeMappings = new HashSet<prUniFreeTenderTypeMapping>();
            srChequesSerialNumbers = new HashSet<srChequesSerialNumber>();
            tpDispOrderHeaderExtensions = new HashSet<tpDispOrderHeaderExtension>();
            tpInvoiceHeaderExtensions = new HashSet<tpInvoiceHeaderExtension>();
            trBankPaymentListLines = new HashSet<trBankPaymentListLine>();
            trCreditCardPaymentLines = new HashSet<trCreditCardPaymentLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

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

        public virtual ICollection<cdBankDesc> cdBankDescs { get; set; }
        public virtual ICollection<cdCreditCardType> cdCreditCardTypes { get; set; }
        public virtual ICollection<dfBankPOSReturnsRule> dfBankPOSReturnsRules { get; set; }
        public virtual ICollection<dfPaynetCompany> dfPaynetCompanys { get; set; }
        public virtual ICollection<prBankBranch> prBankBranchs { get; set; }
        public virtual ICollection<prBankPOSAccounts> prBankPOSAccountss { get; set; }
        public virtual ICollection<prBankPosID> prBankPosIDs { get; set; }
        public virtual ICollection<prBankPOSProviderConvert> prBankPOSProviderConverts { get; set; }
        public virtual ICollection<prCreditCardTypeGLAccs> prCreditCardTypeGLAccss { get; set; }
        public virtual ICollection<prCreditCardValidity> prCreditCardValiditys { get; set; }
        public virtual ICollection<prCustomerDBSAccount> prCustomerDBSAccounts { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<prOnlineDBSLimit> prOnlineDBSLimits { get; set; }
        public virtual ICollection<prOnlineDBSLimitHistory> prOnlineDBSLimitHistorys { get; set; }
        public virtual ICollection<prUniFreeTenderTypeMapping> prUniFreeTenderTypeMappings { get; set; }
        public virtual ICollection<srChequesSerialNumber> srChequesSerialNumbers { get; set; }
        public virtual ICollection<tpDispOrderHeaderExtension> tpDispOrderHeaderExtensions { get; set; }
        public virtual ICollection<tpInvoiceHeaderExtension> tpInvoiceHeaderExtensions { get; set; }
        public virtual ICollection<trBankPaymentListLine> trBankPaymentListLines { get; set; }
        public virtual ICollection<trCreditCardPaymentLine> trCreditCardPaymentLines { get; set; }
    }
}
