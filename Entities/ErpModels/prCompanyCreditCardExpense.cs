using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCompanyCreditCardExpense")]
    public partial class prCompanyCreditCardExpense
    {
        public prCompanyCreditCardExpense()
        {
        }

        [Key]
        [Required]
        public Guid CompanyCreditCardExpenseID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyCreditCardCode { get; set; }

        [Required]
        public DateTime DocumentDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Description { get; set; }

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
        public virtual cdCompanyCreditCard cdCompanyCreditCard { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
