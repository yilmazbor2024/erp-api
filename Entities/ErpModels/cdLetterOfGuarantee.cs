using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLetterOfGuarantee")]
    public partial class cdLetterOfGuarantee
    {
        public cdLetterOfGuarantee()
        {
            prLetterOfGuaranteeAttributes = new HashSet<prLetterOfGuaranteeAttribute>();
        }

        [Key]
        [Required]
        public byte LetterOfGuaranteeTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LetterOfGuaranteeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LetterTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BankBranchCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }
 

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DebitGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditGLAccCode { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        public Guid? PostJournalID { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public DateTime ClosingDate { get; set; }

        [Required]
        public bool IsPostingJournalClosed { get; set; }

        public Guid? PostClosingJournalID { get; set; }

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
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdLetterType cdLetterType { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual bsLetterOfGuaranteeType bsLetterOfGuaranteeType { get; set; }
        public virtual prBankBranch prBankBranch { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<prLetterOfGuaranteeAttribute> prLetterOfGuaranteeAttributes { get; set; }
    }
}
