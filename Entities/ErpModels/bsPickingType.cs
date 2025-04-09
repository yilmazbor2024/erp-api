using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPickingType")]
    public partial class bsPickingType
    {
        public bsPickingType()
        {
            bsPickingTypeDescs = new HashSet<bsPickingTypeDesc>();
            trPickingHeaders = new HashSet<trPickingHeader>();
        }

        [Key]
        [Required]
        public byte PickingTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsPickingTypeDesc> bsPickingTypeDescs { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
    }
}
