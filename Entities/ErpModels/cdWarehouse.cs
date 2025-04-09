using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdWarehouse")]
    public partial class cdWarehouse
    {
        public cdWarehouse()
        {
            cdAllocationTemplates = new HashSet<cdAllocationTemplate>();
            cdPOSTerminals = new HashSet<cdPOSTerminal>();
            cdWarehouseDescs = new HashSet<cdWarehouseDesc>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfConsStoreDistributionWarehouses = new HashSet<dfConsStoreDistributionWarehouse>();
            dfOnlineDistributors = new HashSet<dfOnlineDistributor>();
            dfStoreDeliveryWarehouses = new HashSet<dfStoreDeliveryWarehouse>();
            dfStoreDistributionWarehouses = new HashSet<dfStoreDistributionWarehouse>();
            dfStoreSupportWarehouses = new HashSet<dfStoreSupportWarehouse>();
            dfUserAllowedWarehouses = new HashSet<dfUserAllowedWarehouse>();
            dfUserPositions = new HashSet<dfUserPosition>();
            prCustomerStores = new HashSet<prCustomerStore>();
            prDigitalChannelStockConfigurations = new HashSet<prDigitalChannelStockConfiguration>();
            prItemStockLevels = new HashSet<prItemStockLevel>();
            prSections = new HashSet<prSection>();
            prWarehouseChannelTemplateContents = new HashSet<prWarehouseChannelTemplateContent>();
            prWarehouseMapLocations = new HashSet<prWarehouseMapLocation>();
            prWarehousePostalAddresss = new HashSet<prWarehousePostalAddress>();
            prWarehouseProcessFlowRuless = new HashSet<prWarehouseProcessFlowRules>();
            prWarehouseResponsibilityAreas = new HashSet<prWarehouseResponsibilityArea>();
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            srSerialNumbers = new HashSet<srSerialNumber>();
            tpInvoiceadditionalDeliveryProcessesDistances = new HashSet<tpInvoiceadditionalDeliveryProcessesDistance>();
            tpOrderDeliveryDetails = new HashSet<tpOrderDeliveryDetail>();
            tpPurchaseRequisitionClosedByInventorys = new HashSet<tpPurchaseRequisitionClosedByInventory>();
            tpPurchaseRequisitionProposals = new HashSet<tpPurchaseRequisitionProposal>();
            tpSupportResolves = new HashSet<tpSupportResolve>();
            trAllocations = new HashSet<trAllocation>();
            trDepartmentReceiptLines = new HashSet<trDepartmentReceiptLine>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trPurchaseRequisitionHeaders = new HashSet<trPurchaseRequisitionHeader>();
            trPurchaseRequisitionLines = new HashSet<trPurchaseRequisitionLine>();
            trReportedSaleHeaders = new HashSet<trReportedSaleHeader>();
            trReserveHeaders = new HashSet<trReserveHeader>();
            trReserveTransfers = new HashSet<trReserveTransfer>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trStocks = new HashSet<trStock>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trVehicleLoadingHeaders = new HashSet<trVehicleLoadingHeader>();
            trVehicleUnLoadingHeaders = new HashSet<trVehicleUnLoadingHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [Required]
        public byte WarehouseOwnerCode { get; set; }

        [Required]
        public byte WarehouseTypeCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string WarehouseCategoryCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [Required]
        public bool PermitNegativeStock { get; set; }

        [Required]
        public bool WarnNegativeStock { get; set; }

        [Required]
        public bool ControlStockLevel { get; set; }

        [Required]
        public bool WarnStockLevelRate { get; set; }

        [Required]
        public float TotalArea { get; set; }

        [Required]
        public float WarehouseWidth { get; set; }

        [Required]
        public float WarehouseLength { get; set; }

        [Required]
        public float WarehouseHeight { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string URNAddress { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string EShipmentUrnAddress { get; set; }

        [Required]
        public bool UseSection { get; set; }

        [Required]
        public bool PermitRetailSubsequentDelivery { get; set; }

        [Required]
        public bool IsDefault { get; set; }

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
        public virtual bsWarehouseOwner bsWarehouseOwner { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdWarehouseType cdWarehouseType { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdWarehouseCategory cdWarehouseCategory { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<cdAllocationTemplate> cdAllocationTemplates { get; set; }
        public virtual ICollection<cdPOSTerminal> cdPOSTerminals { get; set; }
        public virtual ICollection<cdWarehouseDesc> cdWarehouseDescs { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfConsStoreDistributionWarehouse> dfConsStoreDistributionWarehouses { get; set; }
        public virtual ICollection<dfOnlineDistributor> dfOnlineDistributors { get; set; }
        public virtual ICollection<dfStoreDeliveryWarehouse> dfStoreDeliveryWarehouses { get; set; }
        public virtual ICollection<dfStoreDistributionWarehouse> dfStoreDistributionWarehouses { get; set; }
        public virtual ICollection<dfStoreSupportWarehouse> dfStoreSupportWarehouses { get; set; }
        public virtual ICollection<dfUserAllowedWarehouse> dfUserAllowedWarehouses { get; set; }
        public virtual ICollection<dfUserPosition> dfUserPositions { get; set; }
        public virtual ICollection<prCustomerStore> prCustomerStores { get; set; }
        public virtual ICollection<prDigitalChannelStockConfiguration> prDigitalChannelStockConfigurations { get; set; }
        public virtual ICollection<prItemStockLevel> prItemStockLevels { get; set; }
        public virtual ICollection<prSection> prSections { get; set; }
        public virtual ICollection<prWarehouseChannelTemplateContent> prWarehouseChannelTemplateContents { get; set; }
        public virtual ICollection<prWarehouseMapLocation> prWarehouseMapLocations { get; set; }
        public virtual ICollection<prWarehousePostalAddress> prWarehousePostalAddresss { get; set; }
        public virtual ICollection<prWarehouseProcessFlowRules> prWarehouseProcessFlowRuless { get; set; }
        public virtual ICollection<prWarehouseResponsibilityArea> prWarehouseResponsibilityAreas { get; set; }
        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<srSerialNumber> srSerialNumbers { get; set; }
        public virtual ICollection<tpInvoiceadditionalDeliveryProcessesDistance> tpInvoiceadditionalDeliveryProcessesDistances { get; set; }
        public virtual ICollection<tpOrderDeliveryDetail> tpOrderDeliveryDetails { get; set; }
        public virtual ICollection<tpPurchaseRequisitionClosedByInventory> tpPurchaseRequisitionClosedByInventorys { get; set; }
        public virtual ICollection<tpPurchaseRequisitionProposal> tpPurchaseRequisitionProposals { get; set; }
        public virtual ICollection<tpSupportResolve> tpSupportResolves { get; set; }
        public virtual ICollection<trAllocation> trAllocations { get; set; }
        public virtual ICollection<trDepartmentReceiptLine> trDepartmentReceiptLines { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trPurchaseRequisitionHeader> trPurchaseRequisitionHeaders { get; set; }
        public virtual ICollection<trPurchaseRequisitionLine> trPurchaseRequisitionLines { get; set; }
        public virtual ICollection<trReportedSaleHeader> trReportedSaleHeaders { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
        public virtual ICollection<trReserveTransfer> trReserveTransfers { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trVehicleLoadingHeader> trVehicleLoadingHeaders { get; set; }
        public virtual ICollection<trVehicleUnLoadingHeader> trVehicleUnLoadingHeaders { get; set; }
    }
}
