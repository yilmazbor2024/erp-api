using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trCountrySpecialDay")]
    public partial class trCountrySpecialDay
    {
        public trCountrySpecialDay()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [Key]
        [Required]
        public DateTime SpecialDayDate { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SpecialDayTypeCode { get; set; }

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
        public virtual cdSpecialDayType cdSpecialDayType { get; set; }
        public virtual cdCountry cdCountry { get; set; }

    }
}
