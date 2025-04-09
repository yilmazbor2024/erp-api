using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsProductTypeDesc")]
    public partial class bsProductTypeDesc
    {
        public bsProductTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte ProductTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ProductTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsProductType bsProductType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
