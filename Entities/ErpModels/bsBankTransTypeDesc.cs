using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankTransTypeDesc")]
    public partial class bsBankTransTypeDesc
    {
        public bsBankTransTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte BankTransTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BankTransTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsBankTransType bsBankTransType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
