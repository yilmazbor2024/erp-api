using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsAccountDetailDesc")]
    public partial class bsAccountDetailDesc
    {
        public bsAccountDetailDesc()
        {
        }

        [Key]
        [Required]
        public byte AccountDetail { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string AccountDetailDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsAccountDetail bsAccountDetail { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
