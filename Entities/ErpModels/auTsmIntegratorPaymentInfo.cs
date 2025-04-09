using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auTsmIntegratorPaymentInfo")]
    public partial class auTsmIntegratorPaymentInfo
    {
        public auTsmIntegratorPaymentInfo()
        {
        }

        [Key]
        [Required]
        public Guid TsmIntegratorPaymentInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TsmTransactionID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentType { get; set; }

        [Required]
        public byte InstallmentNo { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CreditId { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Definition { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ReferenceCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string IssuerId { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CardNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AcquirerId { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
