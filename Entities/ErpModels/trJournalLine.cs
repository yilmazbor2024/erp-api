using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trJournalLine")]
    public partial class trJournalLine
    {
        public trJournalLine()
        {
            tpJournalFTAttributes = new HashSet<tpJournalFTAttribute>();
            trJournalInflationAdjustmentLines = new HashSet<trJournalInflationAdjustmentLine>();
            trJournalLineCostCenterRatess = new HashSet<trJournalLineCostCenterRates>();
            trJournalLineCurrencys = new HashSet<trJournalLineCurrency>();
        }

        [Key]
        [Required]
        public Guid JournalLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RefNumber { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public string LineDescription { get; set; }

        [Required]
        public bool IsFixedExpense { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [Required]
        public bool CreateCurrAccDebit { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CurrAccDescription { get; set; }

        [Required]
        public byte DocumentTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentTypeDescription { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentMethod { get; set; }

        [Required]
        public float TaxRate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WithHoldingTaxTypeCode { get; set; }

        [Required]
        public byte DOVRate1 { get; set; }

        [Required]
        public byte DOVRate2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [Required]
        public short TaxExemptionCode { get; set; }

        [Required]
        public double DebitQty { get; set; }

        [Required]
        public double CreditQty { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [Required]
        public int LedgerLineNumber { get; set; }

        [Required]
        public Guid JournalHeaderID { get; set; }

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
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual bsDocumentType bsDocumentType { get; set; }
        public virtual bsWithHoldingTaxType bsWithHoldingTaxType { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual trJournalHeader trJournalHeader { get; set; }
        public virtual bsTaxExemption bsTaxExemption { get; set; }

        public virtual ICollection<tpJournalFTAttribute> tpJournalFTAttributes { get; set; }
        public virtual ICollection<trJournalInflationAdjustmentLine> trJournalInflationAdjustmentLines { get; set; }
        public virtual ICollection<trJournalLineCostCenterRates> trJournalLineCostCenterRatess { get; set; }
        public virtual ICollection<trJournalLineCurrency> trJournalLineCurrencys { get; set; }
    }
}
