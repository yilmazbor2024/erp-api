using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdGLAcc")]
    public partial class cdGLAcc
    {
        public cdGLAcc()
        {
            cdCompanyCreditCards = new HashSet<cdCompanyCreditCard>();
            cdGLAccDescs = new HashSet<cdGLAccDesc>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            hrEmployeePayrollProfiles = new HashSet<hrEmployeePayrollProfile>();
            prBankAdditionalChargeTypeGLAccss = new HashSet<prBankAdditionalChargeTypeGLAccs>();
            prBankPOSGLAccss = new HashSet<prBankPOSGLAccs>();
            prChequeGLAccss = new HashSet<prChequeGLAccs>();
            prCreditCardTypeGLAccss = new HashSet<prCreditCardTypeGLAccs>();
            prCurrAccGLAccounts = new HashSet<prCurrAccGLAccount>();
            prDeclarationGLAccss = new HashSet<prDeclarationGLAccs>();
            prDiscountTypeGLAccss = new HashSet<prDiscountTypeGLAccs>();
            prDOVGLAccss = new HashSet<prDOVGLAccs>();
            prGLAccAttributes = new HashSet<prGLAccAttribute>();
            prGLAccAvailableForeignCurrencyTranss = new HashSet<prGLAccAvailableForeignCurrencyTrans>();
            prGLAccAvailableJournalTypeSubs = new HashSet<prGLAccAvailableJournalTypeSub>();
            prGLAccNotess = new HashSet<prGLAccNotes>();
            prGLAccOnlineBanks = new HashSet<prGLAccOnlineBank>();
            prGLAccUserWarnings = new HashSet<prGLAccUserWarning>();
            prGLReflectionAccounts = new HashSet<prGLReflectionAccount>();
            prImportFileExpenses = new HashSet<prImportFileExpense>();
            prImportFileGLAccss = new HashSet<prImportFileGLAccs>();
            prItemAccountGrGLAccss = new HashSet<prItemAccountGrGLAccs>();
            prItemCostCenters = new HashSet<prItemCostCenter>();
            prItemCostCenterRatess = new HashSet<prItemCostCenterRates>();
            prMT940ProcessRuless = new HashSet<prMT940ProcessRules>();
            prNotesGLAccss = new HashSet<prNotesGLAccs>();
            prOfficeGLAccss = new HashSet<prOfficeGLAccs>();
            prPaymentProviderGLAccss = new HashSet<prPaymentProviderGLAccs>();
            prPCTGLAccss = new HashSet<prPCTGLAccs>();
            prStoreBankPOSGLAccss = new HashSet<prStoreBankPOSGLAccs>();
            prStoreCustomerGLAccounts = new HashSet<prStoreCustomerGLAccount>();
            prVatGLAccss = new HashSet<prVatGLAccs>();
            prWorkPlaceGLAccss = new HashSet<prWorkPlaceGLAccs>();
            trBankCreditHeaders = new HashSet<trBankCreditHeader>();
            trBankCreditLines = new HashSet<trBankCreditLine>();
            trBankLines = new HashSet<trBankLine>();
            trBudgets = new HashSet<trBudget>();
            trCashLines = new HashSet<trCashLine>();
            trCostCenterDistributionss = new HashSet<trCostCenterDistributions>();
            trExpenseAccrualLines = new HashSet<trExpenseAccrualLine>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trJournalHeaders = new HashSet<trJournalHeader>();
            trJournalInflationAdjustmentHeaders = new HashSet<trJournalInflationAdjustmentHeader>();
            trJournalLines = new HashSet<trJournalLine>();
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

     

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccMainCode { get; set; }

      
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccSubCode1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccSubCode2 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string GLAccSubCode3 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ReverseBalanceGLAccCode { get; set; }

        [Required]
        public bool ForceGLType { get; set; }

        [Required]
        public bool ForceQty { get; set; }

        [Required]
        public bool ForceCostCenter { get; set; }

        [Required]
        public bool AllowForeignCurrencyTrans { get; set; }

        [Required]
        public bool IsStampDutyAcc { get; set; }

        [Required]
        public bool IsIndividualAcc { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [Required]
        public byte TaxPaymentTypeCode { get; set; }

        [Required]
        public byte TaxPaymentAccTypeCode { get; set; }

        [Required]
        public bool IsNonMonetaryItem { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InflationAdjustmentDiffGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InflationAdjustmentGLAccCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public DateTime LockedDate { get; set; }

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

        // Navigation Properties
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual cdGLAccMain cdGLAccMain { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsTaxPaymentAccType bsTaxPaymentAccType { get; set; }
        public virtual bsTaxPaymentType bsTaxPaymentType { get; set; }
        public virtual cdGLAccSub cdGLAccSub { get; set; }

        public virtual ICollection<cdCompanyCreditCard> cdCompanyCreditCards { get; set; }
        public virtual ICollection<cdGLAccDesc> cdGLAccDescs { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<hrEmployeePayrollProfile> hrEmployeePayrollProfiles { get; set; }
        public virtual ICollection<prBankAdditionalChargeTypeGLAccs> prBankAdditionalChargeTypeGLAccss { get; set; }
        public virtual ICollection<prBankPOSGLAccs> prBankPOSGLAccss { get; set; }
        public virtual ICollection<prChequeGLAccs> prChequeGLAccss { get; set; }
        public virtual ICollection<prCreditCardTypeGLAccs> prCreditCardTypeGLAccss { get; set; }
        public virtual ICollection<prCurrAccGLAccount> prCurrAccGLAccounts { get; set; }
        public virtual ICollection<prDeclarationGLAccs> prDeclarationGLAccss { get; set; }
        public virtual ICollection<prDiscountTypeGLAccs> prDiscountTypeGLAccss { get; set; }
        public virtual ICollection<prDOVGLAccs> prDOVGLAccss { get; set; }
        public virtual ICollection<prGLAccAttribute> prGLAccAttributes { get; set; }
        public virtual ICollection<prGLAccAvailableForeignCurrencyTrans> prGLAccAvailableForeignCurrencyTranss { get; set; }
        public virtual ICollection<prGLAccAvailableJournalTypeSub> prGLAccAvailableJournalTypeSubs { get; set; }
        public virtual ICollection<prGLAccNotes> prGLAccNotess { get; set; }
        public virtual ICollection<prGLAccOnlineBank> prGLAccOnlineBanks { get; set; }
        public virtual ICollection<prGLAccUserWarning> prGLAccUserWarnings { get; set; }
        public virtual ICollection<prGLReflectionAccount> prGLReflectionAccounts { get; set; }
        public virtual ICollection<prImportFileExpense> prImportFileExpenses { get; set; }
        public virtual ICollection<prImportFileGLAccs> prImportFileGLAccss { get; set; }
        public virtual ICollection<prItemAccountGrGLAccs> prItemAccountGrGLAccss { get; set; }
        public virtual ICollection<prItemCostCenter> prItemCostCenters { get; set; }
        public virtual ICollection<prItemCostCenterRates> prItemCostCenterRatess { get; set; }
        public virtual ICollection<prMT940ProcessRules> prMT940ProcessRuless { get; set; }
        public virtual ICollection<prNotesGLAccs> prNotesGLAccss { get; set; }
        public virtual ICollection<prOfficeGLAccs> prOfficeGLAccss { get; set; }
        public virtual ICollection<prPaymentProviderGLAccs> prPaymentProviderGLAccss { get; set; }
        public virtual ICollection<prPCTGLAccs> prPCTGLAccss { get; set; }
        public virtual ICollection<prStoreBankPOSGLAccs> prStoreBankPOSGLAccss { get; set; }
        public virtual ICollection<prStoreCustomerGLAccount> prStoreCustomerGLAccounts { get; set; }
        public virtual ICollection<prVatGLAccs> prVatGLAccss { get; set; }
        public virtual ICollection<prWorkPlaceGLAccs> prWorkPlaceGLAccss { get; set; }
        public virtual ICollection<trBankCreditHeader> trBankCreditHeaders { get; set; }
        public virtual ICollection<trBankCreditLine> trBankCreditLines { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trBudget> trBudgets { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trCostCenterDistributions> trCostCenterDistributionss { get; set; }
        public virtual ICollection<trExpenseAccrualLine> trExpenseAccrualLines { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trJournalHeader> trJournalHeaders { get; set; }
        public virtual ICollection<trJournalInflationAdjustmentHeader> trJournalInflationAdjustmentHeaders { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
    }
}
