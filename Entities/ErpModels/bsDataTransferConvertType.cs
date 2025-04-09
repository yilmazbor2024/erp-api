using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDataTransferConvertType")]
    public partial class bsDataTransferConvertType
    {
        public bsDataTransferConvertType()
        {
            cdDataTransferConverts = new HashSet<cdDataTransferConvert>();
            cdDataTransferConvertForAttributes = new HashSet<cdDataTransferConvertForAttribute>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ConvertTypeCode { get; set; }

        [Required]
        public byte Type { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<cdDataTransferConvert> cdDataTransferConverts { get; set; }
        public virtual ICollection<cdDataTransferConvertForAttribute> cdDataTransferConvertForAttributes { get; set; }
    }
}
