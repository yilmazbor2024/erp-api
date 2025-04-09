using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsTaxPaymentTypeDesc")]
    public partial class bsTaxPaymentTypeDesc
    {
        public bsTaxPaymentTypeDesc()
        {
        }

        [Key]
        [Required]
        public byte TaxPaymentTypeCode { get; set; }

        [Key]
        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object TaxPaymentTypeDescription { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        // Navigation Properties
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual bsTaxPaymentType bsTaxPaymentType { get; set; }

    }
}
