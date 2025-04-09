using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRollNoteType")]
    public partial class cdRollNoteType
    {
        public cdRollNoteType()
        {
            cdRollNoteTypeDescs = new HashSet<cdRollNoteTypeDesc>();
            prRollNotess = new HashSet<prRollNotes>();
        }

        [Key]
        [Required]
        public byte RollNoteTypeCode { get; set; }

        [Required]
        public bool IsRequired { get; set; }

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

        public virtual ICollection<cdRollNoteTypeDesc> cdRollNoteTypeDescs { get; set; }
        public virtual ICollection<prRollNotes> prRollNotess { get; set; }
    }
}
