using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models{
    [Table("prCurrAccPostalAddress")]
    public class prCurrAccPostalAddress
    {
        [Key]
        public int PostalAddressID { get; set; }

        [StringLength(20)]
        public string CityCode { get; set; }

        [StringLength(20)]
        public string DistrictCode { get; set; }

        // Navigation Property
        public virtual prCurrAccDefault CurrAccDefault { get; set; }
    }
} 