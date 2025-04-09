using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountOfferApplyDesc")]
    public partial class bsDiscountOfferApplyDesc
    {
        public bsDiscountOfferApplyDesc()
        {
        }

        [Key]
        [Required]
        public byte DiscountOfferApplyCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DiscountOfferApplyDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsDiscountOfferApply bsDiscountOfferApply { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
