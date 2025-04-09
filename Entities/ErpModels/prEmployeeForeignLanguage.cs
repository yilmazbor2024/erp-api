using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prEmployeeForeignLanguage")]
    public partial class prEmployeeForeignLanguage
    {
        public prEmployeeForeignLanguage()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ForeignLanguageCode { get; set; }

        [Required]
        public byte ReadKnowLevelCode { get; set; }

        [Required]
        public byte WriteKnowLevelCode { get; set; }

        [Required]
        public byte SpeakKnowLevelCode { get; set; }

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
        public virtual cdKnowLevel cdKnowLevel { get; set; }
        public virtual cdForeignLanguage cdForeignLanguage { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
