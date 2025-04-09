using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsBankPOSImportTypeDesc")]
    public partial class bsBankPOSImportTypeDesc
    {
        public bsBankPOSImportTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte BankPOSImportTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string BankPOSImportTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsBankPOSImportType bsBankPOSImportType { get; set; }

    }
}
