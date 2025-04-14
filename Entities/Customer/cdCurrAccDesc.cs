using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models
{
    [Table("cdCurrAccDesc")]
    public class cdCurrAccDesc
    {
        [Key]
        [Column(Order = 1)]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string CurrAccCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string LangCode { get; set; }

        [StringLength(60)]
        public string CurrAccDescription { get; set; }

        // Navigation Property
        [ForeignKey("CurrAccTypeCode,CurrAccCode")]
        public virtual cdCurrAcc CurrAcc { get; set; }
    }
} 