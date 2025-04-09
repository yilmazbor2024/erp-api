using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prItemVariant")]
    public partial class prItemVariant
    {
        public prItemVariant()
        {
            cdBOMs = new HashSet<cdBOM>();
            cdRolls = new HashSet<cdRoll>();
            e_InboxShipmentLineV3Itemss = new HashSet<e_InboxShipmentLineV3Items>();
            prBOMContents = new HashSet<prBOMContent>();
            prItemBarcodes = new HashSet<prItemBarcode>();
            prItemBatchBarcodes = new HashSet<prItemBatchBarcode>();
            prItemMeasuresOfVolumes = new HashSet<prItemMeasuresOfVolume>();
            prItemSerialNumberPools = new HashSet<prItemSerialNumberPool>();
            prItemStockLevels = new HashSet<prItemStockLevel>();
            prLinkedProductContents = new HashSet<prLinkedProductContent>();
            prMarketPlaceItemVariants = new HashSet<prMarketPlaceItemVariant>();
            tpPurchaseRequisitionClosedByInventorys = new HashSet<tpPurchaseRequisitionClosedByInventory>();
            tpSupportResolveMaterials = new HashSet<tpSupportResolveMaterial>();
            trAllocationProducts = new HashSet<trAllocationProduct>();
            trContractProducts = new HashSet<trContractProduct>();
            trDepartmentReceiptLines = new HashSet<trDepartmentReceiptLine>();
            trInnerLines = new HashSet<trInnerLine>();
            trInnerLineInventoryTransfers = new HashSet<trInnerLineInventoryTransfer>();
            trInnerOrderLines = new HashSet<trInnerOrderLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trItemTestLines = new HashSet<trItemTestLine>();
            trOrderAsnLines = new HashSet<trOrderAsnLine>();
            trOrderLines = new HashSet<trOrderLine>();
            trProposalLines = new HashSet<trProposalLine>();
            trReportedSaleLines = new HashSet<trReportedSaleLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
            trStocks = new HashSet<trStock>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trTransferPlanProducts = new HashSet<trTransferPlanProduct>();
            trVehicleUnLoadingLines = new HashSet<trVehicleUnLoadingLine>();
        }

        [Key]
        [Required]
        public byte ItemTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ItemCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim1Code { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim2Code { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ItemDim3Code { get; set; }

        [Required]
        public long PLU { get; set; }

        [Required]
        public bool UseInternet { get; set; }

        [Required]
        public byte IsSalesOrderClosed { get; set; }

        [Required]
        public bool IsStoreOrderClosed { get; set; }

        [Required]
        public bool IsPurchaseOrderClosed { get; set; }

        [Required]
        public bool IsLocked { get; set; }

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
        public virtual cdItem cdItem { get; set; }
        public virtual cdItemDim3 cdItemDim3 { get; set; }
        public virtual cdItemDim2 cdItemDim2 { get; set; }
        public virtual cdItemDim1 cdItemDim1 { get; set; }
        public virtual cdColor cdColor { get; set; }

        public virtual ICollection<cdBOM> cdBOMs { get; set; }
        public virtual ICollection<cdRoll> cdRolls { get; set; }
        public virtual ICollection<e_InboxShipmentLineV3Items> e_InboxShipmentLineV3Itemss { get; set; }
        public virtual ICollection<prBOMContent> prBOMContents { get; set; }
        public virtual ICollection<prItemBarcode> prItemBarcodes { get; set; }
        public virtual ICollection<prItemBatchBarcode> prItemBatchBarcodes { get; set; }
        public virtual ICollection<prItemMeasuresOfVolume> prItemMeasuresOfVolumes { get; set; }
        public virtual ICollection<prItemSerialNumberPool> prItemSerialNumberPools { get; set; }
        public virtual ICollection<prItemStockLevel> prItemStockLevels { get; set; }
        public virtual ICollection<prLinkedProductContent> prLinkedProductContents { get; set; }
        public virtual ICollection<prMarketPlaceItemVariant> prMarketPlaceItemVariants { get; set; }
        public virtual ICollection<tpPurchaseRequisitionClosedByInventory> tpPurchaseRequisitionClosedByInventorys { get; set; }
        public virtual ICollection<tpSupportResolveMaterial> tpSupportResolveMaterials { get; set; }
        public virtual ICollection<trAllocationProduct> trAllocationProducts { get; set; }
        public virtual ICollection<trContractProduct> trContractProducts { get; set; }
        public virtual ICollection<trDepartmentReceiptLine> trDepartmentReceiptLines { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
        public virtual ICollection<trInnerLineInventoryTransfer> trInnerLineInventoryTransfers { get; set; }
        public virtual ICollection<trInnerOrderLine> trInnerOrderLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trItemTestLine> trItemTestLines { get; set; }
        public virtual ICollection<trOrderAsnLine> trOrderAsnLines { get; set; }
        public virtual ICollection<trOrderLine> trOrderLines { get; set; }
        public virtual ICollection<trProposalLine> trProposalLines { get; set; }
        public virtual ICollection<trReportedSaleLine> trReportedSaleLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trTransferPlanProduct> trTransferPlanProducts { get; set; }
        public virtual ICollection<trVehicleUnLoadingLine> trVehicleUnLoadingLines { get; set; }
    }
}
