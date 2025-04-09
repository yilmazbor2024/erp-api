using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsItemTypeDesc")]
    public partial class bsItemTypeDesc
    {
        public bsItemTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ItemTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsItemType bsItemType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
