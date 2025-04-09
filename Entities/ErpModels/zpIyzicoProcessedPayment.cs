using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpIyzicoProcessedPayment")]
    public partial class zpIyzicoProcessedPayment
    {
        public zpIyzicoProcessedPayment()
        {
        }

        [Key]
        [Required]
        public Guid IyzicoProcessedPaymentID { get; set; }

        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentId { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentTransactionId { get; set; }

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

    }
}
