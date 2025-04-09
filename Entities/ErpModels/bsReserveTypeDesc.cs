using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsReserveTypeDesc")]
    public partial class bsReserveTypeDesc
    {
        public bsReserveTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte ReserveTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ReserveTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsReserveType bsReserveType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
