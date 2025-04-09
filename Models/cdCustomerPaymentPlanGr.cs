using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdCustomerPaymentPlanGr")]
    public class cdCustomerPaymentPlanGr
    {
        [Key]
        [StringLength(30)]
        public string CustomerPaymentPlanGrCode { get; set; }

        public bool IsBlocked { get; set; }

        // Navigation Properties
        public virtual cdCustomerPaymentPlanGrDesc CustomerPaymentPlanGrDesc { get; set; }
    }
} 