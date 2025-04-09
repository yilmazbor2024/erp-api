using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prUniFreeTenderTypeMapping")]
    public partial class prUniFreeTenderTypeMapping
    {
        public prUniFreeTenderTypeMapping()
        {
        }

        [Key]
        [Required]
        public Guid UniFreeTenderTypeMappingID { get; set; }

        [Required]
        public byte PaymentTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BankCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentProviderCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TenderTypeCode { get; set; }

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
        public virtual cdUniFreeTenderType cdUniFreeTenderType { get; set; }
        public virtual cdBank cdBank { get; set; }
        public virtual cdPaymentProvider cdPaymentProvider { get; set; }
        public virtual bsPaymentType bsPaymentType { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

    }
}
