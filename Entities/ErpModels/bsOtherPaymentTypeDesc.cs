using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsOtherPaymentTypeDesc")]
    public partial class bsOtherPaymentTypeDesc
    {
        public bsOtherPaymentTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte OtherPaymentTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string OtherPaymentTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsOtherPaymentType bsOtherPaymentType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
