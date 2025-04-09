using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsLinkedProductTypeDesc")]
    public partial class bsLinkedProductTypeDesc
    {
        public bsLinkedProductTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte LinkedProductTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LinkedProductTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsLinkedProductType bsLinkedProductType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
