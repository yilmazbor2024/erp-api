using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpDistanceSaleBankPayment")]
    public partial class tpDistanceSaleBankPayment
    {
        public tpDistanceSaleBankPayment()
        {
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BankPaymentCode { get; set; }

        public string BankPaymentMessage { get; set; }

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
        public virtual trOrderHeader trOrderHeader { get; set; }

    }
}
