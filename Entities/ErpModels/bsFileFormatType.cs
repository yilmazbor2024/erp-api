using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsFileFormatType")]
    public partial class bsFileFormatType
    {
        public bsFileFormatType()
        {
            bsFileFormatTypeDescs = new HashSet<bsFileFormatTypeDesc>();
            rpExternalItemFileHeaders = new HashSet<rpExternalItemFileHeader>();
        }

        [Key]
        [Required]
        public byte FileFormatTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsFileFormatTypeDesc> bsFileFormatTypeDescs { get; set; }
        public virtual ICollection<rpExternalItemFileHeader> rpExternalItemFileHeaders { get; set; }
    }
}
