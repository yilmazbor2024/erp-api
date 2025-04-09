using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCreditCardPaymentType")]
    public partial class bsCreditCardPaymentType
    {
        public bsCreditCardPaymentType()
        {
            bsCreditCardPaymentTypeDescs = new HashSet<bsCreditCardPaymentTypeDesc>();
            trCreditCardPaymentHeaders = new HashSet<trCreditCardPaymentHeader>();
        }

        [Key]
        [Required]
        public byte CreditCardPaymentTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCreditCardPaymentTypeDesc> bsCreditCardPaymentTypeDescs { get; set; }
        public virtual ICollection<trCreditCardPaymentHeader> trCreditCardPaymentHeaders { get; set; }
    }
}
