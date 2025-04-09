using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsFormatType")]
    public partial class bsFormatType
    {
        public bsFormatType()
        {
            bsFormatTypeDescs = new HashSet<bsFormatTypeDesc>();
        }

        [Key]
        [Required]
        public int FormatTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsFormatTypeDesc> bsFormatTypeDescs { get; set; }
    }
}
