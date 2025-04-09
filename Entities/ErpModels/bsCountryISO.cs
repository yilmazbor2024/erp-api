using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCountryISO")]
    public partial class bsCountryISO
    {
        public bsCountryISO()
        {
            cdCountrys = new HashSet<cdCountry>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryISOCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CountryName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Alpha2Code { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string Alpha3Code { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdCountry> cdCountrys { get; set; }
    }
}
