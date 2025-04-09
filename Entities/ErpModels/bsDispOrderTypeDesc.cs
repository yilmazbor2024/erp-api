using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsDispOrderTypeDesc")]
    public partial class bsDispOrderTypeDesc
    {
        public bsDispOrderTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte DispOrderTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DispOrderTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsDispOrderType bsDispOrderType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
