using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpCreditCardPaymentHeaderOnlineBankIntegration")]
    public partial class tpCreditCardPaymentHeaderOnlineBankIntegration
    {
        public tpCreditCardPaymentHeaderOnlineBankIntegration()
        {
        }

        [Key]
        [Required]
        public Guid CreditCardPaymentHeaderID { get; set; }

        [Key]
        [Required]
        public Guid OnlineBankTransactionID { get; set; }

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
        public virtual zpOnlineBankCreditCardPaymentTransaction zpOnlineBankCreditCardPaymentTransaction { get; set; }
        public virtual trCreditCardPaymentHeader trCreditCardPaymentHeader { get; set; }

    }
}
