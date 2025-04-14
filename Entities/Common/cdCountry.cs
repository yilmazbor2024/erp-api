using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models  
{
    [Table("cdCountry")]
    public class cdCountry
    {
        [Key]
        [StringLength(10)]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(3)]
        public string ISOCode { get; set; }

        [Required]
        [StringLength(3)]
        public string ISOCode3 { get; set; }

        [Required]
        public short PhoneCode { get; set; }

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
    }
} 