using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("prCurrAccDefault")]
    public class prCurrAccDefault
    {
        [Key]
        [Column(Order = 1)]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string CurrAccCode { get; set; }

        public int PostalAddressID { get; set; }

        // Navigation Properties
        [ForeignKey("CurrAccTypeCode,CurrAccCode")]
        public virtual cdCurrAcc CurrAcc { get; set; }

        [ForeignKey("PostalAddressID")]
        public virtual prCurrAccPostalAddress PostalAddress { get; set; }
    }
} 