using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankPaymentListLine")]
    public partial class trBankPaymentListLine
    {
        public trBankPaymentListLine()
        {
            tpBankPaymentListFTAttributes = new HashSet<tpBankPaymentListFTAttribute>();
        }

        [Key]
        [Required]
        public Guid BankPaymentListLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public byte BankCurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankCurrAccCode { get; set; }

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

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string IBAN { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public Guid? DebitLineID { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ConfirmedUserName { get; set; }

        [Required]
        public DateTime ConfirmedDate { get; set; }

        [Required]
        public bool IsCanceled { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CancelReasonDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CanceledUserName { get; set; }

        [Required]
        public DateTime CancelDate { get; set; }

        [Required]
        public bool IsBankPaymentInstructionCreated { get; set; }

        [Required]
        public Guid BankPaymentListHeaderID { get; set; }

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
        public virtual cdBank cdBank { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual trDebitLine trDebitLine { get; set; }
        public virtual cdGLType cdGLType { get; set; }
        public virtual prBankBranch prBankBranch { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trBankPaymentListHeader trBankPaymentListHeader { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpBankPaymentListFTAttribute> tpBankPaymentListFTAttributes { get; set; }
    }
}
