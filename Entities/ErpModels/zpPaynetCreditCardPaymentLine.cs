using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpPaynetCreditCardPaymentLine")]
    public partial class zpPaynetCreditCardPaymentLine
    {
        public zpPaynetCreditCardPaymentLine()
        {
        }

        [Key]
        [Required]
        public Guid PaynetCreditCardPaymentLineID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CreditCardNum { get; set; }

        [Required]
        public byte CreditCardInstallmentCount { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string POSProvisionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentProviderCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime InstallmentStartDate { get; set; }

        [Required]
        public byte pos_type { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string bank_id { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string bdid { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaynetXactID { get; set; }

        [Required]
        public Guid OrderHeaderID { get; set; }

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

    }
}
