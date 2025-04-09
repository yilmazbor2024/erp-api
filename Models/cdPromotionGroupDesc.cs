using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdPromotionGroupDesc")]
    public class cdPromotionGroupDesc
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string PromotionGroupCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string LangCode { get; set; }

        [Required]
        [StringLength(100)]
        public string PromotionGroupDescription { get; set; }

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
        [ForeignKey("PromotionGroupCode")]
        public virtual cdPromotionGroup PromotionGroup { get; set; }
    }
} 