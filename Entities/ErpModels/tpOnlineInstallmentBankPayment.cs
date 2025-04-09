using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOnlineInstallmentBankPayment")]
    public partial class tpOnlineInstallmentBankPayment
    {
        public tpOnlineInstallmentBankPayment()
        {
        }

        [Key]
        [Required]
        public Guid OnlineInstallmentBankPaymentID { get; set; }

        [Required]
        public byte CustomerTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomerCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankPaymentCode { get; set; }

        public string BankPaymentMessage { get; set; }

        public string DebitAndOrderPaymentLineIDs { get; set; }

        [Required]
        public bool PaymentCompleted { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
