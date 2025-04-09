using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsLetterOfGuaranteeType")]
    public partial class bsLetterOfGuaranteeType
    {
        public bsLetterOfGuaranteeType()
        {
            bsLetterOfGuaranteeTypeDescs = new HashSet<bsLetterOfGuaranteeTypeDesc>();
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            cdLetterOfGuaranteeAttributeTypes = new HashSet<cdLetterOfGuaranteeAttributeType>();
            srCodeNumberLetterOfGuarantees = new HashSet<srCodeNumberLetterOfGuarantee>();
        }

        [Key]
        [Required]
        public byte LetterOfGuaranteeTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsLetterOfGuaranteeTypeDesc> bsLetterOfGuaranteeTypeDescs { get; set; }
        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<cdLetterOfGuaranteeAttributeType> cdLetterOfGuaranteeAttributeTypes { get; set; }
        public virtual ICollection<srCodeNumberLetterOfGuarantee> srCodeNumberLetterOfGuarantees { get; set; }
    }
}
