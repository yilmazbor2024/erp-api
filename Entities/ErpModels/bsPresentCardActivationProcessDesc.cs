using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsPresentCardActivationProcessDesc")]
    public partial class bsPresentCardActivationProcessDesc
    {
        public bsPresentCardActivationProcessDesc()
        {
        }

        [Key]
        [Required]
        public byte ActivationProcessCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ActivationProcessDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsPresentCardActivationProcess bsPresentCardActivationProcess { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
