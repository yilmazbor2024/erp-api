using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prLetterOfGuaranteeAttribute")]
    public partial class prLetterOfGuaranteeAttribute
    {
        public prLetterOfGuaranteeAttribute()
        {
        }

        [Key]
        [Required]
        public byte LetterOfGuaranteeTypeCode { get; set; }

       
        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LetterOfGuaranteeCode { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string AttributeCode { get; set; }

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
        public virtual cdLetterOfGuarantee cdLetterOfGuarantee { get; set; }
        public virtual cdLetterOfGuaranteeAttribute cdLetterOfGuaranteeAttribute { get; set; }

    }
}
