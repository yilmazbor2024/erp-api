using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsWorkplacePropertyStatusDesc")]
    public partial class bsWorkplacePropertyStatusDesc
    {
        public bsWorkplacePropertyStatusDesc()
        {
        }

        [Key]
        [Required]
        public byte WorkplacePropertyStatusCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string WorkplacePropertyStatusDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsWorkplacePropertyStatus bsWorkplacePropertyStatus { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
