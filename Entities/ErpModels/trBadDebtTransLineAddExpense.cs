using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtTransLineAddExpense")]
    public partial class trBadDebtTransLineAddExpense
    {
        public trBadDebtTransLineAddExpense()
        {
            trBadDebtTransAddExpenseDebitss = new HashSet<trBadDebtTransAddExpenseDebits>();
        }

        [Key]
        [Required]
        public Guid BadDebtTransLineAddExpenseID { get; set; }

        [Required]
        public Guid BadDebtTransLineID { get; set; }

        [Required]
        public byte ItemTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool CreateInvoice { get; set; }

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
        public virtual cdItem cdItem { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual trBadDebtTransLine trBadDebtTransLine { get; set; }

        public virtual ICollection<trBadDebtTransAddExpenseDebits> trBadDebtTransAddExpenseDebitss { get; set; }
    }
}
