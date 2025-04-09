using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPresentCardActivationTypeDesc")]
    public partial class bsPresentCardActivationTypeDesc
    {
        public bsPresentCardActivationTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte PresentCardActivationTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string PresentCardActivationTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsPresentCardActivationType bsPresentCardActivationType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
