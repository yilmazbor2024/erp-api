using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsNebimV3WindowsServicesDesc")]
    public partial class bsNebimV3WindowsServicesDesc
    {
        public bsNebimV3WindowsServicesDesc()
        {
        }

        [Key]
        [Required]
        public byte NebimV3WindowsServicesCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string NebimV3WindowsServicesDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsNebimV3WindowsServices bsNebimV3WindowsServices { get; set; }

    }
}
