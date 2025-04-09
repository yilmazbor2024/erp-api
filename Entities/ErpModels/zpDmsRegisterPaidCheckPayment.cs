using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("zpDmsRegisterPaidCheckPayment")]
    public partial class zpDmsRegisterPaidCheckPayment
    {
        public zpDmsRegisterPaidCheckPayment()
        {
        }

        [Key]
        [Required]
        public Guid RegisterPaidCheckPaymentID { get; set; }

        [Required]
        public Guid RegisterPaidCheckID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PaymentType { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ApplicationName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PaymentName { get; set; }

        [Required]
        public bool IsFinancial { get; set; }

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
