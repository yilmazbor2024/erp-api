using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpBulutTahsilatCreditCardVPOSPaymentList")]
    public partial class zpBulutTahsilatCreditCardVPOSPaymentList
    {
        public zpBulutTahsilatCreditCardVPOSPaymentList()
        {
        }

        [Key]
        [Required]
        public Guid CorrelationID { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public long TransactionID { get; set; }

        public string JsonData { get; set; }

        [Required]
        public bool PaymentIntegrated { get; set; }

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
