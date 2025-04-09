using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdCustomerPaymentPlanGrDesc")]
    public class cdCustomerPaymentPlanGrDesc
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CustomerPaymentPlanGrCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string LangCode { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerPaymentPlanGrDescription { get; set; }

        [Required]
        [StringLength(20)]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(20)]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        [ForeignKey("CustomerPaymentPlanGrCode")]
        public virtual cdCustomerPaymentPlanGr CustomerPaymentPlanGr { get; set; }
    }
} 