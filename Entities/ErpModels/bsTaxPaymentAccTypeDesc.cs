using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTaxPaymentAccTypeDesc")]
    public partial class bsTaxPaymentAccTypeDesc
    {
        public bsTaxPaymentAccTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte TaxPaymentAccTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string TaxPaymentAccTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual bsTaxPaymentAccType bsTaxPaymentAccType { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }

    }
}
