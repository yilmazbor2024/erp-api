using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfGlobalDefault")]
    public partial class dfGlobalDefault
    {
        public dfGlobalDefault()
        {
            dfGlobalDataMatrixs = new HashSet<dfGlobalDataMatrix>();
            dfGlobalFolders = new HashSet<dfGlobalFolder>();
            dfGlobalItemSizes = new HashSet<dfGlobalItemSize>();
            dfGlobalMernisUsers = new HashSet<dfGlobalMernisUser>();
        }

        [Key]
        [Required]
        public byte GlobalDefaultCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string IndustryCode { get; set; }

        [Required]
        public byte ItemDimTypeCode { get; set; }

        [Required]
        public bool UseBOM { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string LangCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyCurrencyCode { get; set; }

        [Required]
        public byte ExchangeTypeCode { get; set; }

        [Required]
        public bool UseMultipleLocalCurrency { get; set; }

        [Required]
        public bool UseMultipleLanguage { get; set; }

        [Required]
        public bool UseMultipleCurrency { get; set; }

        [Required]
        public bool UseApparelIndustryFeature { get; set; }

        [Required]
        public byte PolicyReatilCustomerShopping { get; set; }

        [Required]
        public byte PolicyCustomerShopping { get; set; }

        [Required]
        public byte PolicyEmployeeShopping { get; set; }

        [Required]
        public byte PolicyRetailCustomerEdit { get; set; }

        [Required]
        public byte PolicyVendorSharing { get; set; }

        [Required]
        public byte PolicyCustomerPayment { get; set; }

        [Required]
        public byte ProductHierarchyLevelCount { get; set; }

        [Required]
        public bool OpenProductCardFromPHDefault { get; set; }

        [Required]
        public bool AutoGenProductCode { get; set; }

        [Required]
        public byte ProductCodeSize { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string ProductCodeFormula { get; set; }

        [Required]
        public bool OpenProductVariantFromSet { get; set; }

        [Required]
        public bool AutoGenMaterialCode { get; set; }

        [Required]
        public byte MaterialCodeSize { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string MaterialCodeFormula { get; set; }

        [Required]
        public byte StoreHierarchyLevelCount { get; set; }

        [Required]
        public bool UpdateClientsAutomatically { get; set; }

        [Required]
        public bool ActivatePendingPoint { get; set; }

        [Required]
        public decimal PendingPointLimit { get; set; }

        [Required]
        public bool ActivatePointBasedDiscountVoucher { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountPointTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DiscountVoucherTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ReturnDiscountVoucherTypeCode { get; set; }

        [Required]
        public decimal DiscountVoucherLimit { get; set; }

        [Required]
        public decimal MinVoucherAmount { get; set; }

        [Required]
        public decimal MaxVoucherAmount { get; set; }

        [Required]
        public bool DontAllowDuplicateCustomerIdentityNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BarcodeTypeCodeForRollNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchBarcodeTypeCode { get; set; }

        [Required]
        public bool SaveProgramUseTrace { get; set; }

        [Required]
        public bool UseAgentPerformance { get; set; }

        [Required]
        public bool UseOpticalIndustryFeature { get; set; }

        [Required]
        public bool UseMedicalIndustryFeature { get; set; }

        [Required]
        public bool UseOpticalSutContribution { get; set; }

        [Required]
        public bool DisableV3UserOnEmployeeEmployeeResignation { get; set; }

        [Required]
        public bool DisableActiveDirectoryUserOnEmployeeResignation { get; set; }

        [Required]
        public bool EnableUserAccountOnCancelEmployeeResignation { get; set; }

        [Required]
        public bool CheckOutTransactionsOnLoad { get; set; }

        [Required]
        public bool UpdateReportDB { get; set; }

        [Required]
        public bool Use2FA { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object SecretKey2FA { get; set; }

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
        public bool UsePDEnc { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object PDEncHash { get; set; }

        // Navigation Properties
        public virtual bsPolicyCustomerPayment bsPolicyCustomerPayment { get; set; }
        public virtual cdExchangeType cdExchangeType { get; set; }
        public virtual cdDiscountPointType cdDiscountPointType { get; set; }
        public virtual bsPolicyCustomerSharing bsPolicyCustomerSharing { get; set; }
        public virtual cdCurrency cdCurrency { get; set; }
        public virtual cdDataLanguage cdDataLanguage { get; set; }
        public virtual cdIndustry cdIndustry { get; set; }
        public virtual bsPolicyCustomerEdit bsPolicyCustomerEdit { get; set; }
        public virtual bsItemDimType bsItemDimType { get; set; }
        public virtual bsPolicyVendorSharing bsPolicyVendorSharing { get; set; }
        public virtual cdDiscountVoucherType cdDiscountVoucherType { get; set; }

        public virtual ICollection<dfGlobalDataMatrix> dfGlobalDataMatrixs { get; set; }
        public virtual ICollection<dfGlobalFolder> dfGlobalFolders { get; set; }
        public virtual ICollection<dfGlobalItemSize> dfGlobalItemSizes { get; set; }
        public virtual ICollection<dfGlobalMernisUser> dfGlobalMernisUsers { get; set; }
    }
}
