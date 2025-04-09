using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trJournalHeader")]
    public partial class trJournalHeader
    {
        public trJournalHeader()
        {
            prCompanyCreditCardEarnedPointss = new HashSet<prCompanyCreditCardEarnedPoints>();
            prFixedAssetExpenses = new HashSet<prFixedAssetExpense>();
            tpBankCreditRotativeInterestRatess = new HashSet<tpBankCreditRotativeInterestRates>();
            tpJournalATAttributes = new HashSet<tpJournalATAttribute>();
            tpJournalTaxIncurreds = new HashSet<tpJournalTaxIncurred>();
            trExpenseAccrualInflationAdjustmentLines = new HashSet<trExpenseAccrualInflationAdjustmentLine>();
            trJournalInflationAdjustmentHeaders = new HashSet<trJournalInflationAdjustmentHeader>();
            trJournalLedgerEntryNumbers = new HashSet<trJournalLedgerEntryNumber>();
            trJournalLines = new HashSet<trJournalLine>();
        }

        [Key]
        [Required]
        public Guid JournalHeaderID { get; set; }

        [Required]
        public byte JournalTypeCode { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public short JournalTypeSubCode { get; set; }

        [Required]
        public object JournalNumber { get; set; }

        [Required]
        public long LedgerEntryNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CashGLAccCode { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsForeignCurrencyTransaction { get; set; }

        [Required]
        public bool IsDiffCurrencyEachLine { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsJournalPrinted { get; set; }

        [Required]
        public bool IsGLPrinted { get; set; }

        [Required]
        public object FromOfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FromStoreCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

        [Required]
        public bool IsPostingApproved { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdJournalTypeSub cdJournalTypeSub { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual bsJournalType bsJournalType { get; set; }

        public virtual ICollection<prCompanyCreditCardEarnedPoints> prCompanyCreditCardEarnedPointss { get; set; }
        public virtual ICollection<prFixedAssetExpense> prFixedAssetExpenses { get; set; }
        public virtual ICollection<tpBankCreditRotativeInterestRates> tpBankCreditRotativeInterestRatess { get; set; }
        public virtual ICollection<tpJournalATAttribute> tpJournalATAttributes { get; set; }
        public virtual ICollection<tpJournalTaxIncurred> tpJournalTaxIncurreds { get; set; }
        public virtual ICollection<trExpenseAccrualInflationAdjustmentLine> trExpenseAccrualInflationAdjustmentLines { get; set; }
        public virtual ICollection<trJournalInflationAdjustmentHeader> trJournalInflationAdjustmentHeaders { get; set; }
        public virtual ICollection<trJournalLedgerEntryNumber> trJournalLedgerEntryNumbers { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
    }
}
