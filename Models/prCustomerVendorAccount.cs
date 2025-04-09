using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("prCustomerVendorAccount")]
    public class prCustomerVendorAccount
    {
        [Key]
        [Column(Order = 1)]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string CurrAccCode { get; set; }

        [StringLength(30)]
        public string VendorCode { get; set; }

        // Navigation Property
        [ForeignKey("CurrAccTypeCode,CurrAccCode")]
        public virtual cdCurrAcc CurrAcc { get; set; }
    }
} 