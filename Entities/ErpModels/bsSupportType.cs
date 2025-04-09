using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsSupportType")]
    public partial class bsSupportType
    {
        public bsSupportType()
        {
            bsSupportTypeDescs = new HashSet<bsSupportTypeDesc>();
            prServiceAvailableSupportTypes = new HashSet<prServiceAvailableSupportType>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
        }

        [Key]
        [Required]
        public byte SupportTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsSupportTypeDesc> bsSupportTypeDescs { get; set; }
        public virtual ICollection<prServiceAvailableSupportType> prServiceAvailableSupportTypes { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
    }
}
