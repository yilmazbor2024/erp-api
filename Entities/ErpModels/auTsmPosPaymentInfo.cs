using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auTsmPosPaymentInfo")]
    public partial class auTsmPosPaymentInfo
    {
        public auTsmPosPaymentInfo()
        {
        }

        [Key]
        [Required]
        public Guid TsmPosPaymentInfoID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TsmTransactionID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [Required]
        public byte PaymentTypeCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SerialNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [Required]
        public byte InstallmentCount { get; set; }

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
