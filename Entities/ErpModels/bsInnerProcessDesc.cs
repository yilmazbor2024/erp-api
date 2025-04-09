using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsInnerProcessDesc")]
    public partial class bsInnerProcessDesc
    {
        public bsInnerProcessDesc()
        {
        }

        [Key]
        [Required]
        public object InnerProcessCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string InnerProcessDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
