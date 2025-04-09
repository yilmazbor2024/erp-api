using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsWorkplaceKindDesc")]
    public partial class bsWorkplaceKindDesc
    {
        public bsWorkplaceKindDesc()
        {
        }

        [Key]
        [Required]
        public byte WorkplaceKindCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WorkplaceKindDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsWorkplaceKind bsWorkplaceKind { get; set; }

    }
}
