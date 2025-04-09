using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDiscountOfferStage")]
    public partial class bsDiscountOfferStage
    {
        public bsDiscountOfferStage()
        {
            bsDiscountOfferStageDescs = new HashSet<bsDiscountOfferStageDesc>();
            prDiscountOfferPaymentProviders = new HashSet<prDiscountOfferPaymentProvider>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
        }

        [Key]
        [Required]
        public byte DiscountOfferStageCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsDiscountOfferStageDesc> bsDiscountOfferStageDescs { get; set; }
        public virtual ICollection<prDiscountOfferPaymentProvider> prDiscountOfferPaymentProviders { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
    }
}
