using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsConfirmationRuleTypeDesc")]
    public partial class bsConfirmationRuleTypeDesc
    {
        public bsConfirmationRuleTypeDesc()
        {
        }

        [Key]
        [Required]
        public object CompanyCode { get; set; }

        [Key]
        [Required]
        public byte ConfirmationRuleTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConfirmationRuleTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsConfirmationRuleType bsConfirmationRuleType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
