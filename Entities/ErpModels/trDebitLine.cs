using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trDebitLine")]
    public partial class trDebitLine
    {
        public trDebitLine()
        {
            tpDebitFTAttributes = new HashSet<tpDebitFTAttribute>();
            tpInnerLineDocuments = new HashSet<tpInnerLineDocument>();
            trBadDebtTransAddExpenseDebitss = new HashSet<trBadDebtTransAddExpenseDebits>();
            trBadDebtTransLineInstalments = new HashSet<trBadDebtTransLineInstalment>();
            trBankPaymentInstructionLines = new HashSet<trBankPaymentInstructionLine>();
            trBankPaymentListLines = new HashSet<trBankPaymentListLine>();
            trDebitLineCurrencys = new HashSet<trDebitLineCurrency>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public Guid DebitLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string DebitReasonCode { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public Guid DebitHeaderID { get; set; }

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
        public virtual trDebitHeader trDebitHeader { get; set; }
        public virtual cdDebitReason cdDebitReason { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

        public virtual ICollection<tpDebitFTAttribute> tpDebitFTAttributes { get; set; }
        public virtual ICollection<tpInnerLineDocument> tpInnerLineDocuments { get; set; }
        public virtual ICollection<trBadDebtTransAddExpenseDebits> trBadDebtTransAddExpenseDebitss { get; set; }
        public virtual ICollection<trBadDebtTransLineInstalment> trBadDebtTransLineInstalments { get; set; }
        public virtual ICollection<trBankPaymentInstructionLine> trBankPaymentInstructionLines { get; set; }
        public virtual ICollection<trBankPaymentListLine> trBankPaymentListLines { get; set; }
        public virtual ICollection<trDebitLineCurrency> trDebitLineCurrencys { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
