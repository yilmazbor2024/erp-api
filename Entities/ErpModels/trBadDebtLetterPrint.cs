using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtLetterPrint")]
    public partial class trBadDebtLetterPrint
    {
        public trBadDebtLetterPrint()
        {
        }

        [Key]
        [Required]
        public Guid BadDebtLetterID { get; set; }

        [Key]
        [Required]
        public byte TransSortOrder { get; set; }

        [Required]
        public bool Printed { get; set; }

        [Required]
        public bool EMailSent { get; set; }

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

        // Navigation Properties
        public virtual trBadDebtLetter trBadDebtLetter { get; set; }

    }
}
