using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBankCreditLine")]
    public partial class trBankCreditLine
    {
        public trBankCreditLine()
        {
            trBankCreditLineCurrencys = new HashSet<trBankCreditLineCurrency>();
            trBankLines = new HashSet<trBankLine>();
        }

        [Key]
        [Required]
        public Guid BankCreditLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public object CompanyCode { get; set; }

      

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string GLAccCode { get; set; }
 
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExpenseGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ReflectionGLAccCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string LineDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CostCenterCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string GLTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public Guid BankCreditHeaderID { get; set; }

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
        public virtual cdGLType cdGLType { get; set; }
        public virtual trBankCreditHeader trBankCreditHeader { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }

        public virtual ICollection<trBankCreditLineCurrency> trBankCreditLineCurrencys { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
    }
}
