using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankPaymentInstructionLine")]
    public partial class trBankPaymentInstructionLine
    {
        public trBankPaymentInstructionLine()
        {
            tpBankPaymentInstructionFTAttributes = new HashSet<tpBankPaymentInstructionFTAttribute>();
            trBankLines = new HashSet<trBankLine>();
        }

        [Key]
        [Required]
        public Guid BankPaymentInstructionLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankAccNo { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IBAN { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Guid? DebitLineID { get; set; }

        [Required]
        public Guid BankPaymentInstructionHeaderID { get; set; }

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
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual prBankBranch prBankBranch { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual trDebitLine trDebitLine { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trBankPaymentInstructionHeader trBankPaymentInstructionHeader { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpBankPaymentInstructionFTAttribute> tpBankPaymentInstructionFTAttributes { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
    }
}
