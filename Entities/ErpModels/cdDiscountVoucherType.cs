using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDiscountVoucherType")]
    public partial class cdDiscountVoucherType
    {
        public cdDiscountVoucherType()
        {
            cdDiscountOffers = new HashSet<cdDiscountOffer>();
            cdDiscountVouchers = new HashSet<cdDiscountVoucher>();
            cdDiscountVoucherTypeDescs = new HashSet<cdDiscountVoucherTypeDesc>();
            dfGlobalDefaults = new HashSet<dfGlobalDefault>();
            prDiscountVoucherTypeNotess = new HashSet<prDiscountVoucherTypeNotes>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountVoucherTypeCode { get; set; }

        [Required]
        public bool IsProvisionRequired { get; set; }

        [Required]
        public bool IsV3Provision { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ConnectionString { get; set; }

        [Required]
        public bool IsWebServiceProvision { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string WebServiceUrl { get; set; }

        [Required]
        public bool VoucherWillBePrintedOnSale { get; set; }

        [Required]
        public bool UsedManuelNumbering { get; set; }

        [Required]
        public bool UseRecordedVouchers { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCode { get; set; }

        [Required]
        public bool IsBearerVoucher { get; set; }

        [Required]
        public byte DiscountVoucherBaseCode { get; set; }

        [Required]
        public bool IsPercentageDiscount { get; set; }

        [Required]
        public bool IsDisposable { get; set; }

        [Required]
        public bool IsUsedOncePerSale { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal MaxVoucherAmount { get; set; }

        [Required]
        public int AmountRoundingDigit { get; set; }

        [Required]
        public bool IgnorePriorityAndAdvantage { get; set; }

        [Required]
        public bool IgnorePriorityAndAdvantageInUse { get; set; }

        [Required]
        public bool CannotChangeVoucherAmount { get; set; }

        [Required]
        public bool IfUsedThenCannotEarn { get; set; }

        [Required]
        public byte DiscountLevelOfUseCode { get; set; }

        [Required]
        public bool CancelCustomerDiscount { get; set; }

        [Required]
        public bool PrintForm { get; set; }

        [Required]
        public bool UseSystemGenerateNumber { get; set; }

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

        [Required]
        public bool ReadDiscountVoucherSerialNumberWithSecure { get; set; }

        // Navigation Properties
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual bsDiscountLevelOfUse bsDiscountLevelOfUse { get; set; }
        public virtual cdBarcodeType cdBarcodeType { get; set; }
        public virtual bsDiscountVoucherBase bsDiscountVoucherBase { get; set; }

        public virtual ICollection<cdDiscountOffer> cdDiscountOffers { get; set; }
        public virtual ICollection<cdDiscountVoucher> cdDiscountVouchers { get; set; }
        public virtual ICollection<cdDiscountVoucherTypeDesc> cdDiscountVoucherTypeDescs { get; set; }
        public virtual ICollection<dfGlobalDefault> dfGlobalDefaults { get; set; }
        public virtual ICollection<prDiscountVoucherTypeNotes> prDiscountVoucherTypeNotess { get; set; }
    }
}
