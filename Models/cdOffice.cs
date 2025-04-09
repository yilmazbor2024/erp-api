using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdOffice")]
    public class cdOffice
    {
        [Key]
        [StringLength(10)]
        public string OfficeCode { get; set; }

        public bool IsBlocked { get; set; }

        // Navigation Properties
        public virtual cdOfficeDesc OfficeDesc { get; set; }
    }
} 