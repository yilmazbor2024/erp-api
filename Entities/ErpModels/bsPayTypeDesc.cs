using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPayTypeDesc")]
    public partial class bsPayTypeDesc
    {
        public bsPayTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte PayTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PayTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsPayType bsPayType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
