using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsConfirmationTypeDesc")]
    public partial class bsConfirmationTypeDesc
    {
        public bsConfirmationTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte ConfirmationTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConfirmationTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsConfirmationType bsConfirmationType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
