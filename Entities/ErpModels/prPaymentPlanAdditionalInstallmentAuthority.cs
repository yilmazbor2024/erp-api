using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prPaymentPlanAdditionalInstallmentAuthority")]
    public partial class prPaymentPlanAdditionalInstallmentAuthority
    {
        public prPaymentPlanAdditionalInstallmentAuthority()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentPlanCode { get; set; }

        [Key]
        [Required]
        public byte AdditionalInstallmentCount { get; set; }

        [Required]
        public byte AuthorityMethod { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string Password { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ContactFirstLastName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PhoneNumberForSMS { get; set; }

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

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdPaymentPlan cdPaymentPlan { get; set; }

    }
}
