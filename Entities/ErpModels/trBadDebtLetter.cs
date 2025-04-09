using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trBadDebtLetter")]
    public partial class trBadDebtLetter
    {
        public trBadDebtLetter()
        {
            trBadDebtLetterPrints = new HashSet<trBadDebtLetterPrint>();
        }

        [Key]
        [Required]
        public Guid BadDebtLetterID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BadDebtLetterTypeCode { get; set; }

        [Required]
        public bool IsClosed { get; set; }

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
        public virtual cdBadDebtLetterType cdBadDebtLetterType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trBadDebtLetterPrint> trBadDebtLetterPrints { get; set; }
    }
}
