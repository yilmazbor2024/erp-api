using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiscountOffer")]
    public partial class cdDiscountOffer
    {
        public cdDiscountOffer()
        {
            cdDiscountOfferDescs = new HashSet<cdDiscountOfferDesc>();
            prDiscountOfferActiveLogs = new HashSet<prDiscountOfferActiveLog>();
            prDiscountOfferAttributes = new HashSet<prDiscountOfferAttribute>();
            prDiscountOfferDescriptions = new HashSet<prDiscountOfferDescription>();
            prDiscountOfferNotess = new HashSet<prDiscountOfferNotes>();
            prDiscountOfferParameterValues = new HashSet<prDiscountOfferParameterValue>();
            prDiscountOfferPasswords = new HashSet<prDiscountOfferPassword>();
            prDiscountOfferPaymentProviders = new HashSet<prDiscountOfferPaymentProvider>();
            prDiscountOfferRuless = new HashSet<prDiscountOfferRules>();
            prDiscountOfferTurnoverTargets = new HashSet<prDiscountOfferTurnoverTarget>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferCode { get; set; }

        [Required]
        public byte DiscountOfferTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountOfferMethodCode { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object ParameteredFieldsValue { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountVoucherTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountPointTypeCode { get; set; }

        [Required]
        public byte DiscountOfferApplyCode { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public bool CheckEmployeeShoppingLimit { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsValidRetailInstallmentSales { get; set; }

        [Required]
        public bool IgnorePriorityAndAdvantage { get; set; }

        [Required]
        public bool IsActive { get; set; }

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
        public virtual bsDiscountOfferApply bsDiscountOfferApply { get; set; }
        public virtual cdDiscountPointType cdDiscountPointType { get; set; }
        public virtual bsDiscountOfferType bsDiscountOfferType { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual bsDiscountOfferMethod bsDiscountOfferMethod { get; set; }
        public virtual bsCurrAccType bsCurrAccType { get; set; }
        public virtual cdDiscountVoucherType cdDiscountVoucherType { get; set; }

        public virtual ICollection<cdDiscountOfferDesc> cdDiscountOfferDescs { get; set; }
        public virtual ICollection<prDiscountOfferActiveLog> prDiscountOfferActiveLogs { get; set; }
        public virtual ICollection<prDiscountOfferAttribute> prDiscountOfferAttributes { get; set; }
        public virtual ICollection<prDiscountOfferDescription> prDiscountOfferDescriptions { get; set; }
        public virtual ICollection<prDiscountOfferNotes> prDiscountOfferNotess { get; set; }
        public virtual ICollection<prDiscountOfferParameterValue> prDiscountOfferParameterValues { get; set; }
        public virtual ICollection<prDiscountOfferPassword> prDiscountOfferPasswords { get; set; }
        public virtual ICollection<prDiscountOfferPaymentProvider> prDiscountOfferPaymentProviders { get; set; }
        public virtual ICollection<prDiscountOfferRules> prDiscountOfferRuless { get; set; }
        public virtual ICollection<prDiscountOfferTurnoverTarget> prDiscountOfferTurnoverTargets { get; set; }
    }
}
