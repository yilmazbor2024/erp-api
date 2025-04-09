using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEditMaskDesc")]
    public partial class bsEditMaskDesc
    {
        public bsEditMaskDesc()
        {
        }

        [Key]
        [Required]
        public int EditMaskCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EditMaskDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsEditMask bsEditMask { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
