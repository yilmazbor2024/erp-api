using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Entities
{
    [Table("CurrencyDescriptions")]
    public class CurrencyDescription
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string CurrencyCode { get; set; }
        
        [Required]
        [StringLength(5)]
        public string LangCode { get; set; } // "TR", "EN", vb.
        
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
