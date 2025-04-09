using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsInnerOrderTypeDesc")]
    public partial class bsInnerOrderTypeDesc
    {
        public bsInnerOrderTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte InnerOrderTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string InnerOrderTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsInnerOrderType bsInnerOrderType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
