using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsStandardBarcodeType")]
    public partial class bsStandardBarcodeType
    {
        public bsStandardBarcodeType()
        {
            bsStandardBarcodeTypeDescs = new HashSet<bsStandardBarcodeTypeDesc>();
            cdBarcodeTypes = new HashSet<cdBarcodeType>();
        }

        [Key]
        [Required]
        public byte StandardBarcodeTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsStandardBarcodeTypeDesc> bsStandardBarcodeTypeDescs { get; set; }
        public virtual ICollection<cdBarcodeType> cdBarcodeTypes { get; set; }
    }
}
