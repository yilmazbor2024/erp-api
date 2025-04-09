using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountOfferMethodDesc")]
    public partial class bsDiscountOfferMethodDesc
    {
        public bsDiscountOfferMethodDesc()
        {
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferMethodCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountOfferMethodDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsDiscountOfferMethod bsDiscountOfferMethod { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
