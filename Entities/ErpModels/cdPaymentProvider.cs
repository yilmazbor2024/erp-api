using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPaymentProvider")]
    public partial class cdPaymentProvider
    {
        public cdPaymentProvider()
        {
            cdPaymentProviderDescs = new HashSet<cdPaymentProviderDesc>();
            prDiscountOfferPaymentProviders = new HashSet<prDiscountOfferPaymentProvider>();
            prPaymentProviderConverts = new HashSet<prPaymentProviderConvert>();
            prPaymentProviderGLAccss = new HashSet<prPaymentProviderGLAccs>();
            prUniFreeTenderTypeMappings = new HashSet<prUniFreeTenderTypeMapping>();
            tpOrderDistanceSalesCorrelationss = new HashSet<tpOrderDistanceSalesCorrelations>();
            trCreditCardPaymentLines = new HashSet<trCreditCardPaymentLine>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PaymentProviderCode { get; set; }

        [Required]
        public bool IsCreditCardProvider { get; set; }

        [Required]
        public bool IsOKCApplication { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountPointTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

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
        public virtual cdDiscountPointType cdDiscountPointType { get; set; }

        public virtual ICollection<cdPaymentProviderDesc> cdPaymentProviderDescs { get; set; }
        public virtual ICollection<prDiscountOfferPaymentProvider> prDiscountOfferPaymentProviders { get; set; }
        public virtual ICollection<prPaymentProviderConvert> prPaymentProviderConverts { get; set; }
        public virtual ICollection<prPaymentProviderGLAccs> prPaymentProviderGLAccss { get; set; }
        public virtual ICollection<prUniFreeTenderTypeMapping> prUniFreeTenderTypeMappings { get; set; }
        public virtual ICollection<tpOrderDistanceSalesCorrelations> tpOrderDistanceSalesCorrelationss { get; set; }
        public virtual ICollection<trCreditCardPaymentLine> trCreditCardPaymentLines { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
    }
}
