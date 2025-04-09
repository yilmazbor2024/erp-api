using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prRollNotes")]
    public partial class prRollNotes
    {
        public prRollNotes()
        {
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string RollNumber { get; set; }

        [Key]
        [Required]
        public byte RollNoteTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string RollNote { get; set; }

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
        public virtual cdRollNoteType cdRollNoteType { get; set; }
        public virtual cdRoll cdRoll { get; set; }

    }
}
