using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccUBLExtensions")]
    public partial class prCurrAccUBLExtensions
    {
        public prCurrAccUBLExtensions()
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
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string UBLExtensionField { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string SchemeID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FieldDefaultValue { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FieldDescription { get; set; }

        [Required]
        public byte TransType { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Key]
        [Required]
        public byte ProcessType { get; set; }

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
        public virtual bsUBLExtensions bsUBLExtensions { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
