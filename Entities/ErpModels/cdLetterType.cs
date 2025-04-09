using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLetterType")]
    public partial class cdLetterType
    {
        public cdLetterType()
        {
            cdLetterOfGuarantees = new HashSet<cdLetterOfGuarantee>();
            cdLetterTypeDescs = new HashSet<cdLetterTypeDesc>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LetterTypeCode { get; set; }

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

        public virtual ICollection<cdLetterOfGuarantee> cdLetterOfGuarantees { get; set; }
        public virtual ICollection<cdLetterTypeDesc> cdLetterTypeDescs { get; set; }
    }
}
