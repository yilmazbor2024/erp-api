using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiscountPointType")]
    public partial class cdDiscountPointType
    {
        public cdDiscountPointType()
        {
            cdDiscountOffers = new HashSet<cdDiscountOffer>();
            cdDiscountPointTypeDescs = new HashSet<cdDiscountPointTypeDesc>();
            cdPaymentProviders = new HashSet<cdPaymentProvider>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
            prDiscountPoints = new HashSet<prDiscountPoint>();
            prDiscountPointTypeNotess = new HashSet<prDiscountPointTypeNotes>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountPointTypeCode { get; set; }

        [Required]
        public byte PointBaseCode { get; set; }

        [Required]
        public bool UseProductPoint { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ProductPointTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal PointAmount { get; set; }

        [Required]
        public bool IgnorePriorityAndAdvantage { get; set; }

        [Required]
        public bool IgnorePriorityAndAdvantageInUse { get; set; }

        [Required]
        public byte DiscountLevelOfUseCode { get; set; }

        [Required]
        public bool CancelCustomerDiscount { get; set; }

        [Required]
        public bool IfUsedThenCannotEarn { get; set; }

        [Required]
        public bool UseAsPayment { get; set; }

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
        public virtual bsDiscountLevelOfUse bsDiscountLevelOfUse { get; set; }
        public virtual cdProductPointType cdProductPointType { get; set; }
        public virtual bsPointBase bsPointBase { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }

        public virtual ICollection<cdDiscountOffer> cdDiscountOffers { get; set; }
        public virtual ICollection<cdDiscountPointTypeDesc> cdDiscountPointTypeDescs { get; set; }
        public virtual ICollection<cdPaymentProvider> cdPaymentProviders { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
        public virtual ICollection<prDiscountPoint> prDiscountPoints { get; set; }
        public virtual ICollection<prDiscountPointTypeNotes> prDiscountPointTypeNotess { get; set; }
    }
}
