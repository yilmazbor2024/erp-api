using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdDistrict")]
    public class cdDistrict
    {
        [Key]
        [StringLength(30)]
        public string DistrictCode { get; set; }

        [Required]
        [StringLength(10)]
        public string CityCode { get; set; }

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