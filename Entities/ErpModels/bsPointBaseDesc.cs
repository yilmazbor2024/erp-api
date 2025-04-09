using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPointBaseDesc")]
    public partial class bsPointBaseDesc
    {
        public bsPointBaseDesc()
        {
        }

        [Key]
        [Required]
        public byte PointBaseCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PointBaseDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsPointBase bsPointBase { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
