using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsNebimV3ServicesDesc")]
    public partial class bsNebimV3ServicesDesc
    {
        public bsNebimV3ServicesDesc()
        {
        }

        [Key]
        [Required]
        public byte NebimV3ServicesCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string NebimV3ServicesDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsNebimV3Services bsNebimV3Services { get; set; }

    }
}
