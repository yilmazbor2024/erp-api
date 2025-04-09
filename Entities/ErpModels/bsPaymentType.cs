using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPaymentType")]
    public partial class bsPaymentType
    {
        public bsPaymentType()
        {
            bsPaymentTypeDescs = new HashSet<bsPaymentTypeDesc>();
            prUniFreeTenderTypeMappings = new HashSet<prUniFreeTenderTypeMapping>();
            trPaymentLines = new HashSet<trPaymentLine>();
        }

        [Key]
        [Required]
        public byte PaymentTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPaymentTypeDesc> bsPaymentTypeDescs { get; set; }
        public virtual ICollection<prUniFreeTenderTypeMapping> prUniFreeTenderTypeMappings { get; set; }
        public virtual ICollection<trPaymentLine> trPaymentLines { get; set; }
    }
}
