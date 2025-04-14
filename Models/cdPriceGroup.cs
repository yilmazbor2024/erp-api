using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ErpMobile.Api.Models.Customer;

namespace ErpMobile.Api.Models
{
    [Table("cdPriceGroup")]
    public class cdPriceGroup
    {
        [Key]
        [StringLength(30)]
        public string PriceGroupCode { get; set; }

        [StringLength(10)]
        public string DefaultCurrencyCode { get; set; }

        public bool IsTaxIncluded { get; set; }

        public bool IsBlocked { get; set; }

        // Navigation Properties
        public virtual cdPriceGroupDesc PriceGroupDesc { get; set; }
    }
} 