using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models
{
    [Table("cdCityDesc")]
    public class cdCityDesc
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CityCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string LangCode { get; set; }

        [Required]
        [StringLength(100)]
        public string CityDescription { get; set; }

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
        [ForeignKey("CityCode")]
        public virtual cdCity City { get; set; }
    }
} 