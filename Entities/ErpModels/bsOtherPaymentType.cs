using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsOtherPaymentType")]
    public partial class bsOtherPaymentType
    {
        public bsOtherPaymentType()
        {
            bsOtherPaymentTypeDescs = new HashSet<bsOtherPaymentTypeDesc>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
        }

        [Key]
        [Required]
        public byte OtherPaymentTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsOtherPaymentTypeDesc> bsOtherPaymentTypeDescs { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
    }
}
