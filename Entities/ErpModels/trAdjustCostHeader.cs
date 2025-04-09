using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trAdjustCostHeader")]
    public partial class trAdjustCostHeader
    {
        public trAdjustCostHeader()
        {
            trAdjustCostBankLines = new HashSet<trAdjustCostBankLine>();
            trAdjustCostExpenseInvoiceLines = new HashSet<trAdjustCostExpenseInvoiceLine>();
            trAdjustCostExpenseSlipLines = new HashSet<trAdjustCostExpenseSlipLine>();
            trAdjustCostInners = new HashSet<trAdjustCostInner>();
            trAdjustCostInventorys = new HashSet<trAdjustCostInventory>();
            trAdjustCostInvoices = new HashSet<trAdjustCostInvoice>();
            trAdjustCostOrders = new HashSet<trAdjustCostOrder>();
        }

        [Key]
        [Required]
        public Guid AdjustCostHeaderID { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object InnerProcessCode { get; set; }

        [Required]
        public object AdjustCostNumber { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        public Guid? InvoiceHeaderID { get; set; }

        public Guid? ExpenseSlipHeaderID { get; set; }

        public Guid? BankHeaderID { get; set; }

        [Required]
        public byte AdjustCostMethodCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public float Rate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DocCurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LocalCurrencyCode { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

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
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trAdjustCostBankLine> trAdjustCostBankLines { get; set; }
        public virtual ICollection<trAdjustCostExpenseInvoiceLine> trAdjustCostExpenseInvoiceLines { get; set; }
        public virtual ICollection<trAdjustCostExpenseSlipLine> trAdjustCostExpenseSlipLines { get; set; }
        public virtual ICollection<trAdjustCostInner> trAdjustCostInners { get; set; }
        public virtual ICollection<trAdjustCostInventory> trAdjustCostInventorys { get; set; }
        public virtual ICollection<trAdjustCostInvoice> trAdjustCostInvoices { get; set; }
        public virtual ICollection<trAdjustCostOrder> trAdjustCostOrders { get; set; }
    }
}
