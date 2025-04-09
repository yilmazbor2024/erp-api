using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountOfferMethod")]
    public partial class bsDiscountOfferMethod
    {
        public bsDiscountOfferMethod()
        {
            bsDiscountOfferMethodDescs = new HashSet<bsDiscountOfferMethodDesc>();
            cdDiscountOffers = new HashSet<cdDiscountOffer>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferMethodCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ParameteredFields { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ClassName { get; set; }

        [Required]
        public byte DiscountOfferTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsDiscountOfferType bsDiscountOfferType { get; set; }

        public virtual ICollection<bsDiscountOfferMethodDesc> bsDiscountOfferMethodDescs { get; set; }
        public virtual ICollection<cdDiscountOffer> cdDiscountOffers { get; set; }
    }
}
