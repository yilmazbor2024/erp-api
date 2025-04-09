using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdOfficeDesc")]
    public class cdOfficeDesc
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string OfficeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string LangCode { get; set; }

        [Required]
        [StringLength(100)]
        public string OfficeDescription { get; set; }

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

        // Navigation Property
        [ForeignKey("OfficeCode")]
        public virtual cdOffice Office { get; set; }
    }
} 