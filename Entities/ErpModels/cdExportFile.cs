using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdExportFile")]
    public partial class cdExportFile
    {
        public cdExportFile()
        {
            cdExportFileDescs = new HashSet<cdExportFileDesc>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            prExportFileAttributes = new HashSet<prExportFileAttribute>();
            prExportFileIndirectExpenses = new HashSet<prExportFileIndirectExpense>();
            prExportFileInsurances = new HashSet<prExportFileInsurance>();
            prExportFileShippingInfos = new HashSet<prExportFileShippingInfo>();
            prExportFileStatusHistorys = new HashSet<prExportFileStatusHistory>();
            tpBankCreditRelatedExportFiless = new HashSet<tpBankCreditRelatedExportFiles>();
            trBankHeaders = new HashSet<trBankHeader>();
            trBankLines = new HashSet<trBankLine>();
            trBankPaymentInstructionLines = new HashSet<trBankPaymentInstructionLine>();
            trBankPaymentListLines = new HashSet<trBankPaymentListLine>();
            trCashHeaders = new HashSet<trCashHeader>();
            trCashLines = new HashSet<trCashLine>();
            trChequeLines = new HashSet<trChequeLine>();
            trCurrAccBooks = new HashSet<trCurrAccBook>();
            trExpenseAccrualHeaders = new HashSet<trExpenseAccrualHeader>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trItemTestLines = new HashSet<trItemTestLine>();
            trJournalLines = new HashSet<trJournalLine>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trProposalLines = new HashSet<trProposalLine>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trShipmentLines = new HashSet<trShipmentLine>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [Required]
        public object CompanyCode { get; set; }
 

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExtraNumber1 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExtraNumber2 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode2 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LettersOfCreditNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PaymentMethodCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string PaymentMeansCode { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomsOfficesCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomsCompanyCode { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public DateTime ClosingDate { get; set; }

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
        public virtual cdCustomsCompany cdCustomsCompany { get; set; }
        public virtual cdCustomsOffices cdCustomsOffices { get; set; }
        public virtual bsIncoterm bsIncoterm { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual bsPaymentMeans bsPaymentMeans { get; set; }
        public virtual cdPaymentMethod cdPaymentMethod { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdExportFileDesc> cdExportFileDescs { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<prExportFileAttribute> prExportFileAttributes { get; set; }
        public virtual ICollection<prExportFileIndirectExpense> prExportFileIndirectExpenses { get; set; }
        public virtual ICollection<prExportFileInsurance> prExportFileInsurances { get; set; }
        public virtual ICollection<prExportFileShippingInfo> prExportFileShippingInfos { get; set; }
        public virtual ICollection<prExportFileStatusHistory> prExportFileStatusHistorys { get; set; }
        public virtual ICollection<tpBankCreditRelatedExportFiles> tpBankCreditRelatedExportFiless { get; set; }
        public virtual ICollection<trBankHeader> trBankHeaders { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trBankPaymentInstructionLine> trBankPaymentInstructionLines { get; set; }
        public virtual ICollection<trBankPaymentListLine> trBankPaymentListLines { get; set; }
        public virtual ICollection<trCashHeader> trCashHeaders { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trChequeLine> trChequeLines { get; set; }
        public virtual ICollection<trCurrAccBook> trCurrAccBooks { get; set; }
        public virtual ICollection<trExpenseAccrualHeader> trExpenseAccrualHeaders { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trItemTestLine> trItemTestLines { get; set; }
        public virtual ICollection<trJournalLine> trJournalLines { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
    }
}
