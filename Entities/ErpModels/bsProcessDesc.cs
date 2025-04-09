using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsProcessDesc")]
    public partial class bsProcessDesc
    {
        public bsProcessDesc()
        {
        }

        [Key]
        [Required]
        public object ProcessCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ProcessDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
