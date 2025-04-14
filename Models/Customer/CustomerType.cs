using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Customer
{
    public class CustomerType
    {
        [Key]
        [StringLength(3)]
        public string TypeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
    }
} 