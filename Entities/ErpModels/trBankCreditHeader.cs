using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankCreditHeader")]
    public partial class trBankCreditHeader
    {
        public trBankCreditHeader()
        {
            tpBankCreditATAttributes = new HashSet<tpBankCreditATAttribute>();
            tpBankCreditFTAttributes = new HashSet<tpBankCreditFTAttribute>();
            tpBankCreditRelatedChequess = new HashSet<tpBankCreditRelatedCheques>();
            tpBankCreditRelatedExportFiless = new HashSet<tpBankCreditRelatedExportFiles>();
            tpBankCreditRotativeInterestRatess = new HashSet<tpBankCreditRotativeInterestRates>();
            trBankCreditLines = new HashSet<trBankCreditLine>();
            trBankCreditPaymentPlans = new HashSet<trBankCreditPaymentPlan>();
        }

        [Key]
        [Required]
        public Guid BankCreditHeaderID { get; set; }

        [Required]
        public object BankCreditNumber { get; set; }

        [Required]
        public byte BankCreditGuaranteeTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCreditTypeCode { get; set; }

        [Required]
        public bool FreeInstallmentDateEntry { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Required]
        public byte InstallmentCount { get; set; }

        [Required]
        public short DueDayCount { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal CapitalAmount { get; set; }

        [Required]
        public decimal ExpenseCommisionAmount { get; set; }

        [Required]
        public float InterestRate { get; set; }

        [Required]
        public float KKDFRate { get; set; }

        [Required]
        public float BSMVRate { get; set; }

        [Required]
        public float DeductionRate1 { get; set; }

        [Required]
        public float DeductionRate2 { get; set; }

        [Required]
        public float DeductionRate3 { get; set; }

        [Required]
        public object CompanyCode { get; set; }
 
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LongTermBankCreditGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExpenseCommisionGLAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public byte BankOpTypeCode { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        public bool? IsClosed { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdBankCreditType cdBankCreditType { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdBankOpType cdBankOpType { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsBankCreditGuaranteeType bsBankCreditGuaranteeType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpBankCreditATAttribute> tpBankCreditATAttributes { get; set; }
        public virtual ICollection<tpBankCreditFTAttribute> tpBankCreditFTAttributes { get; set; }
        public virtual ICollection<tpBankCreditRelatedCheques> tpBankCreditRelatedChequess { get; set; }
        public virtual ICollection<tpBankCreditRelatedExportFiles> tpBankCreditRelatedExportFiless { get; set; }
        public virtual ICollection<tpBankCreditRotativeInterestRates> tpBankCreditRotativeInterestRatess { get; set; }
        public virtual ICollection<trBankCreditLine> trBankCreditLines { get; set; }
        public virtual ICollection<trBankCreditPaymentPlan> trBankCreditPaymentPlans { get; set; }
    }
}
