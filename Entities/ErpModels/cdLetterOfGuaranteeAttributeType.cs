using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLetterOfGuaranteeAttributeType")]
    public partial class cdLetterOfGuaranteeAttributeType
    {
        public cdLetterOfGuaranteeAttributeType()
        {
            cdLetterOfGuaranteeAttributes = new HashSet<cdLetterOfGuaranteeAttribute>();
            cdLetterOfGuaranteeAttributeTypeDescs = new HashSet<cdLetterOfGuaranteeAttributeTypeDesc>();
        }

        [Key]
        [Required]
        public byte LetterOfGuaranteeTypeCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Description { get; set; }

        [Key]
        [Required]
        public byte AttributeTypeCode { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public bool UseReports { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual bsLetterOfGuaranteeType bsLetterOfGuaranteeType { get; set; }

        public virtual ICollection<cdLetterOfGuaranteeAttribute> cdLetterOfGuaranteeAttributes { get; set; }
        public virtual ICollection<cdLetterOfGuaranteeAttributeTypeDesc> cdLetterOfGuaranteeAttributeTypeDescs { get; set; }
    }
}
