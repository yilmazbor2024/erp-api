using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBadDebtTransTypeDesc")]
    public partial class bsBadDebtTransTypeDesc
    {
        public bsBadDebtTransTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte BadDebtTransTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BadDebtTransTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsBadDebtTransType bsBadDebtTransType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
