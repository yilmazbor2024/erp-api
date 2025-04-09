using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsVendorTypeDesc")]
    public partial class bsVendorTypeDesc
    {
        public bsVendorTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte VendorTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string VendorTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsVendorType bsVendorType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
