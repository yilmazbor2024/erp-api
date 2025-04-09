using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtTransAddExpenseDebits")]
    public partial class trBadDebtTransAddExpenseDebits
    {
        public trBadDebtTransAddExpenseDebits()
        {
        }

        [Key]
        [Required]
        public Guid BadDebtTransLineAddExpenseID { get; set; }

        [Key]
        [Required]
        public Guid DebitLineID { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
        public virtual trDebitLine trDebitLine { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual trBadDebtTransLineAddExpense trBadDebtTransLineAddExpense { get; set; }

    }
}
