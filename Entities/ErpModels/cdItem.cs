using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdItem")]
    public partial class cdItem
    {
        public cdItem()
        {
            cdGiftCards = new HashSet<cdGiftCard>();
            cdItemDescs = new HashSet<cdItemDesc>();
            cdPlasticBagTypes = new HashSet<cdPlasticBagType>();
            dfCarriageExpenseCodess = new HashSet<dfCarriageExpenseCodes>();
            dfInsuaranceExpenseCodess = new HashSet<dfInsuaranceExpenseCodes>();
            dfMacellanSuperappCompanys = new HashSet<dfMacellanSuperappCompany>();
            prCompanyExpenses = new HashSet<prCompanyExpense>();
            prCustomProcessGroupAtts = new HashSet<prCustomProcessGroupAtt>();
            prExportFileIndirectExpenses = new HashSet<prExportFileIndirectExpense>();
            prFixedAssetDepreciationInfos = new HashSet<prFixedAssetDepreciationInfo>();
            prFixedAssetEmployees = new HashSet<prFixedAssetEmployee>();
            prFixedAssetExpenses = new HashSet<prFixedAssetExpense>();
            prFixedAssetInsurances = new HashSet<prFixedAssetInsurance>();
            prFixedAssetPurchasess = new HashSet<prFixedAssetPurchases>();
            prFixedAssetReassessmentRatess = new HashSet<prFixedAssetReassessmentRates>();
            prFixedAssetSaless = new HashSet<prFixedAssetSales>();
            prFixedAssetStatusHistorys = new HashSet<prFixedAssetStatusHistory>();
            prImportFileExpenses = new HashSet<prImportFileExpense>();
            prImportFileIndirectExpenses = new HashSet<prImportFileIndirectExpense>();
            prItemAirportSalesCommissionGroups = new HashSet<prItemAirportSalesCommissionGroup>();
            prItemAlikes = new HashSet<prItemAlike>();
            prItemAttributes = new HashSet<prItemAttribute>();
            prItemBasePrices = new HashSet<prItemBasePrice>();
            prItemColorAttributess = new HashSet<prItemColorAttributes>();
            prItemColorFabricBlends = new HashSet<prItemColorFabricBlend>();
            prItemCompanyBrands = new HashSet<prItemCompanyBrand>();
            prItemCostCenters = new HashSet<prItemCostCenter>();
            prItemCostCenterRatess = new HashSet<prItemCostCenterRates>();
            prItemCrossUnitOfMeasures = new HashSet<prItemCrossUnitOfMeasure>();
            prItemInformations = new HashSet<prItemInformation>();
            prItemNotess = new HashSet<prItemNotes>();
            prItemPhotos = new HashSet<prItemPhoto>();
            prItemRequisitions = new HashSet<prItemRequisition>();
            prItemSerialNumbers = new HashSet<prItemSerialNumber>();
            prItemTextileCareSymbols = new HashSet<prItemTextileCareSymbol>();
            prItemUnAcceptableExpenses = new HashSet<prItemUnAcceptableExpense>();
            prItemVariants = new HashSet<prItemVariant>();
            prLinkedProductContents = new HashSet<prLinkedProductContent>();
            prLinkedProductContentSums = new HashSet<prLinkedProductContentSum>();
            prLinkedProductPropertiess = new HashSet<prLinkedProductProperties>();
            prMarketPlaceProducts = new HashSet<prMarketPlaceProduct>();
            prMarketPlaceProductInformations = new HashSet<prMarketPlaceProductInformation>();
            prMedicalProductImportCountriess = new HashSet<prMedicalProductImportCountries>();
            prMedicalProductOriginCountriess = new HashSet<prMedicalProductOriginCountries>();
            prMedicalProductPropertiess = new HashSet<prMedicalProductProperties>();
            prProductCareWarnings = new HashSet<prProductCareWarning>();
            prProductColorAttributes = new HashSet<prProductColorAttribute>();
            prProductFramePropertiess = new HashSet<prProductFrameProperties>();
            prProductImageURLss = new HashSet<prProductImageURLs>();
            prProductLensPropertiess = new HashSet<prProductLensProperties>();
            prProductLots = new HashSet<prProductLot>();
            prProductLotBarcodes = new HashSet<prProductLotBarcode>();
            prProductPoints = new HashSet<prProductPoint>();
            prProductStatusHistorys = new HashSet<prProductStatusHistory>();
            prServiceAvailableProductLevels = new HashSet<prServiceAvailableProductLevel>();
            prServiceAvailableSupportTypes = new HashSet<prServiceAvailableSupportType>();
            srCodeNumberGiftCards = new HashSet<srCodeNumberGiftCard>();
            tpOrderOpticalProductCustomProcesss = new HashSet<tpOrderOpticalProductCustomProcess>();
            trBadDebtTransLineAddExpenses = new HashSet<trBadDebtTransLineAddExpense>();
            trCostOfGoodsSoldHeaders = new HashSet<trCostOfGoodsSoldHeader>();
            trExpenseSlipLines = new HashSet<trExpenseSlipLine>();
            trFixedAssetBookHeaders = new HashSet<trFixedAssetBookHeader>();
            trInvoiceLineLinkedProducts = new HashSet<trInvoiceLineLinkedProduct>();
            trOrderLineLinkedProducts = new HashSet<trOrderLineLinkedProduct>();
            trPriceListLines = new HashSet<trPriceListLine>();
            trSalesPlanProducts = new HashSet<trSalesPlanProduct>();
            trSupportRequestLines = new HashSet<trSupportRequestLine>();
            trTaxIncurredHeaders = new HashSet<trTaxIncurredHeader>();
            trVendorPriceListLines = new HashSet<trVendorPriceListLine>();
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Required]
        public byte ItemDimTypeCode { get; set; }

        [Required]
        public byte ProductTypeCode { get; set; }

        [Required]
        public int ProductHierarchyID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode2 { get; set; }

        [Required]
        public float UnitConvertRate { get; set; }

        [Required]
        public bool UnitConvertRateNotFixed { get; set; }

        [Required]
        public bool UseRoll { get; set; }

        [Required]
        public bool UseBatch { get; set; }

        [Required]
        public bool UseInternet { get; set; }

        [Required]
        public bool UsePOS { get; set; }

        [Required]
        public bool UseStore { get; set; }

        [Required]
        public bool EnablePartnerCompanies { get; set; }

        [Required]
        public bool UseManufacturing { get; set; }

        [Required]
        public bool UseSerialNumber { get; set; }

        [Required]
        public bool GenerateSerialNumber { get; set; }

        [Required]
        public bool GenerateOpticalDataMatrixCode { get; set; }

        [Required]
        public bool ByWeight { get; set; }

        [Required]
        public short SupplyPeriod { get; set; }

        [Required]
        public short GuaranteePeriod { get; set; }

        [Required]
        public short ShelfLife { get; set; }

        [Required]
        public int OrderLeadTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemAccountGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemTaxGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemPaymentPlanGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDiscountGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemVendorGrCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PromotionGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PromotionGroupCode2 { get; set; }

        [Required]
        public int ProductCollectionGrCode { get; set; }

        [Required]
        public byte StorePriceLevelCode { get; set; }

        [Required]
        public byte PerceptionOfFashionCode { get; set; }

        [Required]
        public byte CommercialRoleCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string StoreCapacityLevelCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomsTariffNumberCode { get; set; }

        [Required]
        public bool IsFixedExpense { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BOMEntityCode { get; set; }

        [Required]
        public byte MaxCreditCardInstallmentCount { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string IGACommissionGroup { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string UniFreeCommissionGroup { get; set; }

        [Required]
        public byte CustomsProductGroupCode { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public DateTime LockedDate { get; set; }

        [Required]
        public byte IsSalesOrderClosed { get; set; }

        [Required]
        public bool IsStoreOrderClosed { get; set; }

        [Required]
        public bool IsPurchaseOrderClosed { get; set; }

        [Required]
        public byte IsSubsequentDeliveryForR { get; set; }

        [Required]
        public byte IsSubsequentDeliveryForRI { get; set; }

        [Required]
        public bool IsUTSDeclaratedItem { get; set; }

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
        public virtual cdBOMEntity cdBOMEntity { get; set; }
        public virtual cdCommercialRole cdCommercialRole { get; set; }
        public virtual cdItemDiscountGr cdItemDiscountGr { get; set; }
        public virtual cdItemPaymentPlanGr cdItemPaymentPlanGr { get; set; }
        public virtual bsCustomsProductGroup bsCustomsProductGroup { get; set; }
        public virtual cdCustomsTariffNumber cdCustomsTariffNumber { get; set; }
        public virtual cdUnitOfMeasure cdUnitOfMeasure { get; set; }
        public virtual bsItemDimType bsItemDimType { get; set; }
        public virtual cdStorePriceLevel cdStorePriceLevel { get; set; }
        public virtual cdItemTaxGr cdItemTaxGr { get; set; }
        public virtual cdPerceptionOfFashion cdPerceptionOfFashion { get; set; }
        public virtual bsProductType bsProductType { get; set; }
        public virtual bsItemType bsItemType { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdPromotionGroup cdPromotionGroup { get; set; }
        public virtual cdItemVendorGr cdItemVendorGr { get; set; }
        public virtual dfProductHierarchy dfProductHierarchy { get; set; }
        public virtual cdProductCollectionGr cdProductCollectionGr { get; set; }
        public virtual cdItemAccountGr cdItemAccountGr { get; set; }
        public virtual cdStoreCapacityLevel cdStoreCapacityLevel { get; set; }

        public virtual ICollection<cdGiftCard> cdGiftCards { get; set; }
        public virtual ICollection<cdItemDesc> cdItemDescs { get; set; }
        public virtual ICollection<cdPlasticBagType> cdPlasticBagTypes { get; set; }
        public virtual ICollection<dfCarriageExpenseCodes> dfCarriageExpenseCodess { get; set; }
        public virtual ICollection<dfInsuaranceExpenseCodes> dfInsuaranceExpenseCodess { get; set; }
        public virtual ICollection<dfMacellanSuperappCompany> dfMacellanSuperappCompanys { get; set; }
        public virtual ICollection<prCompanyExpense> prCompanyExpenses { get; set; }
        public virtual ICollection<prCustomProcessGroupAtt> prCustomProcessGroupAtts { get; set; }
        public virtual ICollection<prExportFileIndirectExpense> prExportFileIndirectExpenses { get; set; }
        public virtual ICollection<prFixedAssetDepreciationInfo> prFixedAssetDepreciationInfos { get; set; }
        public virtual ICollection<prFixedAssetEmployee> prFixedAssetEmployees { get; set; }
        public virtual ICollection<prFixedAssetExpense> prFixedAssetExpenses { get; set; }
        public virtual ICollection<prFixedAssetInsurance> prFixedAssetInsurances { get; set; }
        public virtual ICollection<prFixedAssetPurchases> prFixedAssetPurchasess { get; set; }
        public virtual ICollection<prFixedAssetReassessmentRates> prFixedAssetReassessmentRatess { get; set; }
        public virtual ICollection<prFixedAssetSales> prFixedAssetSaless { get; set; }
        public virtual ICollection<prFixedAssetStatusHistory> prFixedAssetStatusHistorys { get; set; }
        public virtual ICollection<prImportFileExpense> prImportFileExpenses { get; set; }
        public virtual ICollection<prImportFileIndirectExpense> prImportFileIndirectExpenses { get; set; }
        public virtual ICollection<prItemAirportSalesCommissionGroup> prItemAirportSalesCommissionGroups { get; set; }
        public virtual ICollection<prItemAlike> prItemAlikes { get; set; }
        public virtual ICollection<prItemAttribute> prItemAttributes { get; set; }
        public virtual ICollection<prItemBasePrice> prItemBasePrices { get; set; }
        public virtual ICollection<prItemColorAttributes> prItemColorAttributess { get; set; }
        public virtual ICollection<prItemColorFabricBlend> prItemColorFabricBlends { get; set; }
        public virtual ICollection<prItemCompanyBrand> prItemCompanyBrands { get; set; }
        public virtual ICollection<prItemCostCenter> prItemCostCenters { get; set; }
        public virtual ICollection<prItemCostCenterRates> prItemCostCenterRatess { get; set; }
        public virtual ICollection<prItemCrossUnitOfMeasure> prItemCrossUnitOfMeasures { get; set; }
        public virtual ICollection<prItemInformation> prItemInformations { get; set; }
        public virtual ICollection<prItemNotes> prItemNotess { get; set; }
        public virtual ICollection<prItemPhoto> prItemPhotos { get; set; }
        public virtual ICollection<prItemRequisition> prItemRequisitions { get; set; }
        public virtual ICollection<prItemSerialNumber> prItemSerialNumbers { get; set; }
        public virtual ICollection<prItemTextileCareSymbol> prItemTextileCareSymbols { get; set; }
        public virtual ICollection<prItemUnAcceptableExpense> prItemUnAcceptableExpenses { get; set; }
        public virtual ICollection<prItemVariant> prItemVariants { get; set; }
        public virtual ICollection<prLinkedProductContent> prLinkedProductContents { get; set; }
        public virtual ICollection<prLinkedProductContentSum> prLinkedProductContentSums { get; set; }
        public virtual ICollection<prLinkedProductProperties> prLinkedProductPropertiess { get; set; }
        public virtual ICollection<prMarketPlaceProduct> prMarketPlaceProducts { get; set; }
        public virtual ICollection<prMarketPlaceProductInformation> prMarketPlaceProductInformations { get; set; }
        public virtual ICollection<prMedicalProductImportCountries> prMedicalProductImportCountriess { get; set; }
        public virtual ICollection<prMedicalProductOriginCountries> prMedicalProductOriginCountriess { get; set; }
        public virtual ICollection<prMedicalProductProperties> prMedicalProductPropertiess { get; set; }
        public virtual ICollection<prProductCareWarning> prProductCareWarnings { get; set; }
        public virtual ICollection<prProductColorAttribute> prProductColorAttributes { get; set; }
        public virtual ICollection<prProductFrameProperties> prProductFramePropertiess { get; set; }
        public virtual ICollection<prProductImageURLs> prProductImageURLss { get; set; }
        public virtual ICollection<prProductLensProperties> prProductLensPropertiess { get; set; }
        public virtual ICollection<prProductLot> prProductLots { get; set; }
        public virtual ICollection<prProductLotBarcode> prProductLotBarcodes { get; set; }
        public virtual ICollection<prProductPoint> prProductPoints { get; set; }
        public virtual ICollection<prProductStatusHistory> prProductStatusHistorys { get; set; }
        public virtual ICollection<prServiceAvailableProductLevel> prServiceAvailableProductLevels { get; set; }
        public virtual ICollection<prServiceAvailableSupportType> prServiceAvailableSupportTypes { get; set; }
        public virtual ICollection<srCodeNumberGiftCard> srCodeNumberGiftCards { get; set; }
        public virtual ICollection<tpOrderOpticalProductCustomProcess> tpOrderOpticalProductCustomProcesss { get; set; }
        public virtual ICollection<trBadDebtTransLineAddExpense> trBadDebtTransLineAddExpenses { get; set; }
        public virtual ICollection<trCostOfGoodsSoldHeader> trCostOfGoodsSoldHeaders { get; set; }
        public virtual ICollection<trExpenseSlipLine> trExpenseSlipLines { get; set; }
        public virtual ICollection<trFixedAssetBookHeader> trFixedAssetBookHeaders { get; set; }
        public virtual ICollection<trInvoiceLineLinkedProduct> trInvoiceLineLinkedProducts { get; set; }
        public virtual ICollection<trOrderLineLinkedProduct> trOrderLineLinkedProducts { get; set; }
        public virtual ICollection<trPriceListLine> trPriceListLines { get; set; }
        public virtual ICollection<trSalesPlanProduct> trSalesPlanProducts { get; set; }
        public virtual ICollection<trSupportRequestLine> trSupportRequestLines { get; set; }
        public virtual ICollection<trTaxIncurredHeader> trTaxIncurredHeaders { get; set; }
        public virtual ICollection<trVendorPriceListLine> trVendorPriceListLines { get; set; }
    }
}
