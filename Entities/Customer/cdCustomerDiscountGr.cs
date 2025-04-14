using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models
{
    [Table("cdCustomerDiscountGr")]
    public class cdCustomerDiscountGr
    {
        [Key]
        [StringLength(30)]
        public string CustomerDiscountGrCode { get; set; }

        public bool IsBlocked { get; set; }

        // Navigation Properties
        public virtual cdCustomerDiscountGrDesc CustomerDiscountGrDesc { get; set; }
    }
} 