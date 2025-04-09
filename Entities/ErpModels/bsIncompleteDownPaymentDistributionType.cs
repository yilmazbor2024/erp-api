using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsIncompleteDownPaymentDistributionType")]
    public partial class bsIncompleteDownPaymentDistributionType
    {
        public bsIncompleteDownPaymentDistributionType()
        {
            bsIncompleteDownPaymentDistributionTypeDescs = new HashSet<bsIncompleteDownPaymentDistributionTypeDesc>();
            prProcessInfos = new HashSet<prProcessInfo>();
        }

        [Key]
        [Required]
        public byte IncompleteDownPaymentDistributionTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsIncompleteDownPaymentDistributionTypeDesc> bsIncompleteDownPaymentDistributionTypeDescs { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
    }
}
