using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsGiftCardPaymentType")]
    public partial class bsGiftCardPaymentType
    {
        public bsGiftCardPaymentType()
        {
            bsGiftCardPaymentTypeDescs = new HashSet<bsGiftCardPaymentTypeDesc>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
        }

        [Key]
        [Required]
        public byte GiftCardPaymentTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsGiftCardPaymentTypeDesc> bsGiftCardPaymentTypeDescs { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
    }
}
