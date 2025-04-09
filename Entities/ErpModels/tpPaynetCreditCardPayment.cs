using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpPaynetCreditCardPayment")]
    public partial class tpPaynetCreditCardPayment
    {
        public tpPaynetCreditCardPayment()
        {
        }

        [Key]
        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [Required]
        public Guid CorrelationID { get; set; }

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
