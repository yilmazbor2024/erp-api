using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("cdPriceGroupDesc")]
    public class cdPriceGroupDesc
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string PriceGroupCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string LangCode { get; set; }

        [StringLength(60)]
        public string PriceGroupDescription { get; set; }

        // Navigation Property
        [ForeignKey("PriceGroupCode")]
        public virtual cdPriceGroup PriceGroup { get; set; }
    }
} 