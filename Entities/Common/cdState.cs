using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models
{
    [Table("cdState")]
    public class cdState
    {
        [Key]
        [StringLength(10)]
        public string StateCode { get; set; }

        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; }

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
        [ForeignKey("CountryCode")]
        public virtual cdCountry Country { get; set; }
    }
} 