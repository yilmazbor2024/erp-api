using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trExpenseAccrualHeader")]
    public partial class trExpenseAccrualHeader
    {
        public trExpenseAccrualHeader()
        {
            tpExpenseAccrualATAttributes = new HashSet<tpExpenseAccrualATAttribute>();
            tpInvoiceLineExpenseAccruals = new HashSet<tpInvoiceLineExpenseAccrual>();
            trExpenseAccrualLines = new HashSet<trExpenseAccrualLine>();
        }

        [Key]
        [Required]
        public Guid ExpenseAccrualHeaderID { get; set; }

        [Required]
        public bool IsExpense { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [Required]
        public TimeSpan DocumentTime { get; set; }

        [Required]
        public object RefNumber { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public short InstallmentCount { get; set; }

        [Required]
        public short DueDayCount { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }

        public virtual ICollection<tpExpenseAccrualATAttribute> tpExpenseAccrualATAttributes { get; set; }
        public virtual ICollection<tpInvoiceLineExpenseAccrual> tpInvoiceLineExpenseAccruals { get; set; }
        public virtual ICollection<trExpenseAccrualLine> trExpenseAccrualLines { get; set; }
    }
}
