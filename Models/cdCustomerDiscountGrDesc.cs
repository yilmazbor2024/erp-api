using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdCustomerDiscountGrDesc")]
    public class cdCustomerDiscountGrDesc
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string CustomerDiscountGrCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string LangCode { get; set; }

        [StringLength(60)]
        public string CustomerDiscountGrDescription { get; set; }

        // Navigation Property
        [ForeignKey("CustomerDiscountGrCode")]
        public virtual cdCustomerDiscountGr CustomerDiscountGr { get; set; }
    }
} 