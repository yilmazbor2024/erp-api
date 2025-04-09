using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDeviceTypeDesc")]
    public partial class bsDeviceTypeDesc
    {
        public bsDeviceTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte DeviceTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DeviceTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsDeviceType bsDeviceType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
