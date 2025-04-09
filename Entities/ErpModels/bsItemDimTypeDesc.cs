using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsItemDimTypeDesc")]
    public partial class bsItemDimTypeDesc
    {
        public bsItemDimTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte ItemDimTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ItemDimTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsItemDimType bsItemDimType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
