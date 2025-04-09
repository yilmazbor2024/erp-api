using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trExpenseAccrualLine")]
    public partial class trExpenseAccrualLine
    {
        public trExpenseAccrualLine()
        {
            tpExpenseAccrualFTAttributes = new HashSet<tpExpenseAccrualFTAttribute>();
            tpInvoiceLineExpenseAccruals = new HashSet<tpInvoiceLineExpenseAccrual>();
            trExpenseAccrualInflationAdjustmentLines = new HashSet<trExpenseAccrualInflationAdjustmentLine>();
            trExpenseAccrualLineCostCenterRatess = new HashSet<trExpenseAccrualLineCostCenterRates>();
            trExpenseAccrualLineCurrencys = new HashSet<trExpenseAccrualLineCurrency>();
        }

        [Key]
        [Required]
        public Guid ExpenseAccrualLineID { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DebitGLAccCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditGLAccCode { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

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
        public Guid ExpenseAccrualHeaderID { get; set; }

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
        public virtual trExpenseAccrualHeader trExpenseAccrualHeader { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCostCenter cdCostCenter { get; set; }
        public virtual cdGLAcc cdGLAcc { get; set; }

        public virtual ICollection<tpExpenseAccrualFTAttribute> tpExpenseAccrualFTAttributes { get; set; }
        public virtual ICollection<tpInvoiceLineExpenseAccrual> tpInvoiceLineExpenseAccruals { get; set; }
        public virtual ICollection<trExpenseAccrualInflationAdjustmentLine> trExpenseAccrualInflationAdjustmentLines { get; set; }
        public virtual ICollection<trExpenseAccrualLineCostCenterRates> trExpenseAccrualLineCostCenterRatess { get; set; }
        public virtual ICollection<trExpenseAccrualLineCurrency> trExpenseAccrualLineCurrencys { get; set; }
    }
}
