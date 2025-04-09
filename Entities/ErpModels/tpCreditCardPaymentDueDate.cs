using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpCreditCardPaymentDueDate")]
    public partial class tpCreditCardPaymentDueDate
    {
        public tpCreditCardPaymentDueDate()
        {
        }

        [Key]
        [Required]
        public Guid CreditCardPaymentDueDateID { get; set; }

        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [Required]
        public DateTime Duedate { get; set; }

        [Required]
        public DateTime CalculatedDueDate { get; set; }

        [Required]
        public decimal InstallmentAmount { get; set; }

        [Required]
        public decimal ServiceFeeAmount { get; set; }

        [Required]
        public decimal TaxAmount { get; set; }

        [Required]
        public decimal PosPointAmount { get; set; }

        [Required]
        public decimal EarlyPaymentDiscount { get; set; }

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
        public virtual trCreditCardPaymentLine trCreditCardPaymentLine { get; set; }

    }
}
