using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDepreciationMethodDesc")]
    public partial class bsDepreciationMethodDesc
    {
        public bsDepreciationMethodDesc()
        {
        }

        [Key]
        [Required]
        public byte DepreciationMethodCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DepreciationMethodDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsDepreciationMethod bsDepreciationMethod { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
