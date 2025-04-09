using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBadDebtLetterType")]
    public partial class cdBadDebtLetterType
    {
        public cdBadDebtLetterType()
        {
            cdBadDebtLetterTypeDescs = new HashSet<cdBadDebtLetterTypeDesc>();
            trBadDebtLetters = new HashSet<trBadDebtLetter>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BadDebtLetterTypeCode { get; set; }

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

        public virtual ICollection<cdBadDebtLetterTypeDesc> cdBadDebtLetterTypeDescs { get; set; }
        public virtual ICollection<trBadDebtLetter> trBadDebtLetters { get; set; }
    }
}
