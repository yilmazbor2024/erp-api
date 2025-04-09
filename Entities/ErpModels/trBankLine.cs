using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankLine")]
    public partial class trBankLine
    {
        public trBankLine()
        {
            tpBankFTAttributes = new HashSet<tpBankFTAttribute>();
            tpOnlineBankPosPaymentLists = new HashSet<tpOnlineBankPosPaymentList>();
            trAdjustCostBankLines = new HashSet<trAdjustCostBankLine>();
            trBankLineAdditionalCharges = new HashSet<trBankLineAdditionalCharge>();
            trBankLineCostCenterRatess = new HashSet<trBankLineCostCenterRates>();
            trBankLineCurrencys = new HashSet<trBankLineCurrency>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid BankLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

        [Required]
        public bool IsBankDebited { get; set; }

        public byte? CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        public string LineDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankReferenceNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankConfirmationNumber { get; set; }

        [Required]
        public float TaxRate { get; set; }

        [Required]
        public byte EmployeePayTypeCode { get; set; }

        [Required]
        public double Qty { get; set; }

        [Required]
        public byte BankOpTypeCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [Required]
        public byte DocumentTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentTypeDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentMethod { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ProvisionNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RelatedBankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RelatedBankBranchCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RelatedBankAccNo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RelatedIBAN { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string RelatedCurrAccDescription { get; set; }

        [Required]
        public bool IsTransferChargeIncluded { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrAccCurrencyCode { get; set; }

        [Required]
        public double CurrAccExchangeRate { get; set; }

        [Required]
        public decimal CurrAccAmount { get; set; }

        public Guid? BankPaymentInstructionLineID { get; set; }

        public Guid? BankCreditLineID { get; set; }

        [Required]
        public Guid BankHeaderID { get; set; }

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
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trBankCreditLine trBankCreditLine { get; set; }
        public virtual trBankPaymentInstructionLine trBankPaymentInstructionLine { get; set; }
        public virtual bsDocumentType bsDocumentType { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdBankOpType cdBankOpType { get; set; }
        public virtual bsEmployeePayType bsEmployeePayType { get; set; }
        public virtual trBankHeader trBankHeader { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpBankFTAttribute> tpBankFTAttributes { get; set; }
        public virtual ICollection<tpOnlineBankPosPaymentList> tpOnlineBankPosPaymentLists { get; set; }
        public virtual ICollection<trAdjustCostBankLine> trAdjustCostBankLines { get; set; }
        public virtual ICollection<trBankLineAdditionalCharge> trBankLineAdditionalCharges { get; set; }
        public virtual ICollection<trBankLineCostCenterRates> trBankLineCostCenterRatess { get; set; }
        public virtual ICollection<trBankLineCurrency> trBankLineCurrencys { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
