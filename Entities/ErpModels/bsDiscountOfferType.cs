using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountOfferType")]
    public partial class bsDiscountOfferType
    {
        public bsDiscountOfferType()
        {
            bsDiscountOfferMethods = new HashSet<bsDiscountOfferMethod>();
            bsDiscountOfferTypeDescs = new HashSet<bsDiscountOfferTypeDesc>();
            cdDiscountOffers = new HashSet<cdDiscountOffer>();
        }

        [Key]
        [Required]
        public byte DiscountOfferTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDiscountOfferMethod> bsDiscountOfferMethods { get; set; }
        public virtual ICollection<bsDiscountOfferTypeDesc> bsDiscountOfferTypeDescs { get; set; }
        public virtual ICollection<cdDiscountOffer> cdDiscountOffers { get; set; }
    }
}
