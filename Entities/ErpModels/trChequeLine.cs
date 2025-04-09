using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trChequeLine")]
    public partial class trChequeLine
    {
        public trChequeLine()
        {
            tpBankCreditRelatedChequess = new HashSet<tpBankCreditRelatedCheques>();
            tpChequeFTAttributes = new HashSet<tpChequeFTAttribute>();
            trChequeLineCurrencys = new HashSet<trChequeLineCurrency>();
            trOrderAdvancePaymentss = new HashSet<trOrderAdvancePayments>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid ChequeLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public int TransSortOrder { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ChequeCode { get; set; }

        [Required]
        public byte ChequeTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ChequeCurrencyCode { get; set; }

        [Required]
        public double ChequeExchangeRate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [Required]
        public bool BankProtestChargePaid { get; set; }

        [Required]
        public byte EmployeePayTypeCode { get; set; }

        [Required]
        public Guid ChequeHeaderID { get; set; }

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
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdCheque cdCheque { get; set; }
        public virtual bsEmployeePayType bsEmployeePayType { get; set; }
        public virtual trChequeHeader trChequeHeader { get; set; }

        public virtual ICollection<tpBankCreditRelatedCheques> tpBankCreditRelatedChequess { get; set; }
        public virtual ICollection<tpChequeFTAttribute> tpChequeFTAttributes { get; set; }
        public virtual ICollection<trChequeLineCurrency> trChequeLineCurrencys { get; set; }
        public virtual ICollection<trOrderAdvancePayments> trOrderAdvancePaymentss { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
