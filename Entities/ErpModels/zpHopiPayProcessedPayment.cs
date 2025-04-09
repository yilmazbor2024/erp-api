using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpHopiPayProcessedPayment")]
    public partial class zpHopiPayProcessedPayment
    {
        public zpHopiPayProcessedPayment()
        {
        }

        [Key]
        [Required]
        public Guid HopiPayProcessedPaymentID { get; set; }

        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string HopiPayID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public byte Installment { get; set; }

        [Required]
        public byte PaymentType { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string IssuerCardCode { get; set; }

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
