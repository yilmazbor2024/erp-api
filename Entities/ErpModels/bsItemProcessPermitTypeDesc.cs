using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsItemProcessPermitTypeDesc")]
    public partial class bsItemProcessPermitTypeDesc
    {
        public bsItemProcessPermitTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte ItemProcessPermitTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ItemProcessPermitTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsItemProcessPermitType bsItemProcessPermitType { get; set; }

    }
}
