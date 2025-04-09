using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsReserveType")]
    public partial class bsReserveType
    {
        public bsReserveType()
        {
            bsReserveTypeDescs = new HashSet<bsReserveTypeDesc>();
            trReserveHeaders = new HashSet<trReserveHeader>();
        }

        [Key]
        [Required]
        public byte ReserveTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsReserveTypeDesc> bsReserveTypeDescs { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
    }
}
