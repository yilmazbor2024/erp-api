using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLetterOfGuaranteeAttributeTypeDesc")]
    public partial class cdLetterOfGuaranteeAttributeTypeDesc
    {
        public cdLetterOfGuaranteeAttributeTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte LetterOfGuaranteeTypeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AttributeTypeDescription { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CreatedUserName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastUpdatedUserName { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdLetterOfGuaranteeAttributeType cdLetterOfGuaranteeAttributeType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
