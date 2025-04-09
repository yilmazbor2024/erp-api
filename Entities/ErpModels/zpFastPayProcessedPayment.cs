using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpFastPayProcessedPayment")]
    public partial class zpFastPayProcessedPayment
    {
        public zpFastPayProcessedPayment()
        {
        }

        [Key]
        [Required]
        public Guid FastPayProcessedPaymentID { get; set; }

        [Required]
        public Guid CreditCardPaymentLineID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FastPayID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AuthCode { get; set; }

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
