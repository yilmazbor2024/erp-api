using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountOfferTypeDesc")]
    public partial class bsDiscountOfferTypeDesc
    {
        public bsDiscountOfferTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte DiscountOfferTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountOfferTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsDiscountOfferType bsDiscountOfferType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
