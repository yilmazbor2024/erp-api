using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsEmployeeSpecialTypeDesc")]
    public partial class bsEmployeeSpecialTypeDesc
    {
        public bsEmployeeSpecialTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte EmployeeSpecialTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EmployeeSpecialTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsEmployeeSpecialType bsEmployeeSpecialType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
