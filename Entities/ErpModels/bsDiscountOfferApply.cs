using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountOfferApply")]
    public partial class bsDiscountOfferApply
    {
        public bsDiscountOfferApply()
        {
            bsDiscountOfferApplyDescs = new HashSet<bsDiscountOfferApplyDesc>();
            cdDiscountOffers = new HashSet<cdDiscountOffer>();
        }

        [Key]
        [Required]
        public byte DiscountOfferApplyCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDiscountOfferApplyDesc> bsDiscountOfferApplyDescs { get; set; }
        public virtual ICollection<cdDiscountOffer> cdDiscountOffers { get; set; }
    }
}
