using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdColor")]
    public partial class cdColor
    {
        public cdColor()
        {
            cdColorDescs = new HashSet<cdColorDesc>();
            cdPantones = new HashSet<cdPantone>();
            prItemColorAttributess = new HashSet<prItemColorAttributes>();
            prItemColorFabricBlends = new HashSet<prItemColorFabricBlend>();
            prItemListContents = new HashSet<prItemListContent>();
            prItemTextileCareSymbols = new HashSet<prItemTextileCareSymbol>();
            prItemVariants = new HashSet<prItemVariant>();
            prLinkedProductContentSumDetails = new HashSet<prLinkedProductContentSumDetail>();
            prMarketPlaceProducts = new HashSet<prMarketPlaceProduct>();
            prProductColorAttributes = new HashSet<prProductColorAttribute>();
            prProductColorSetContents = new HashSet<prProductColorSetContent>();
            prProductImageURLss = new HashSet<prProductImageURLs>();
            prProductLots = new HashSet<prProductLot>();
            prProductLotBarcodes = new HashSet<prProductLotBarcode>();
            trAllocationProducts = new HashSet<trAllocationProduct>();
            trInnerLineSumDetails = new HashSet<trInnerLineSumDetail>();
            trInnerOrderLineSumDetails = new HashSet<trInnerOrderLineSumDetail>();
            trInvoiceLineSumDetails = new HashSet<trInvoiceLineSumDetail>();
            trOrderAsnLineSumDetails = new HashSet<trOrderAsnLineSumDetail>();
            trOrderLineSumDetails = new HashSet<trOrderLineSumDetail>();
            trPriceListLines = new HashSet<trPriceListLine>();
            trProposalLineSumDetails = new HashSet<trProposalLineSumDetail>();
            trSalesPlanProductQtys = new HashSet<trSalesPlanProductQty>();
            trShipmentLineSumDetails = new HashSet<trShipmentLineSumDetail>();
            trTransferPlanProducts = new HashSet<trTransferPlanProduct>();
            trVendorPriceListLines = new HashSet<trVendorPriceListLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ColorHex { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCatalogCode1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCatalogCode2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ColorCatalogCode3 { get; set; }

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
        public virtual cdColorCatalog cdColorCatalog { get; set; }

        public virtual ICollection<cdColorDesc> cdColorDescs { get; set; }
        public virtual ICollection<cdPantone> cdPantones { get; set; }
        public virtual ICollection<prItemColorAttributes> prItemColorAttributess { get; set; }
        public virtual ICollection<prItemColorFabricBlend> prItemColorFabricBlends { get; set; }
        public virtual ICollection<prItemListContent> prItemListContents { get; set; }
        public virtual ICollection<prItemTextileCareSymbol> prItemTextileCareSymbols { get; set; }
        public virtual ICollection<prItemVariant> prItemVariants { get; set; }
        public virtual ICollection<prLinkedProductContentSumDetail> prLinkedProductContentSumDetails { get; set; }
        public virtual ICollection<prMarketPlaceProduct> prMarketPlaceProducts { get; set; }
        public virtual ICollection<prProductColorAttribute> prProductColorAttributes { get; set; }
        public virtual ICollection<prProductColorSetContent> prProductColorSetContents { get; set; }
        public virtual ICollection<prProductImageURLs> prProductImageURLss { get; set; }
        public virtual ICollection<prProductLot> prProductLots { get; set; }
        public virtual ICollection<prProductLotBarcode> prProductLotBarcodes { get; set; }
        public virtual ICollection<trAllocationProduct> trAllocationProducts { get; set; }
        public virtual ICollection<trInnerLineSumDetail> trInnerLineSumDetails { get; set; }
        public virtual ICollection<trInnerOrderLineSumDetail> trInnerOrderLineSumDetails { get; set; }
        public virtual ICollection<trInvoiceLineSumDetail> trInvoiceLineSumDetails { get; set; }
        public virtual ICollection<trOrderAsnLineSumDetail> trOrderAsnLineSumDetails { get; set; }
        public virtual ICollection<trOrderLineSumDetail> trOrderLineSumDetails { get; set; }
        public virtual ICollection<trPriceListLine> trPriceListLines { get; set; }
        public virtual ICollection<trProposalLineSumDetail> trProposalLineSumDetails { get; set; }
        public virtual ICollection<trSalesPlanProductQty> trSalesPlanProductQtys { get; set; }
        public virtual ICollection<trShipmentLineSumDetail> trShipmentLineSumDetails { get; set; }
        public virtual ICollection<trTransferPlanProduct> trTransferPlanProducts { get; set; }
        public virtual ICollection<trVendorPriceListLine> trVendorPriceListLines { get; set; }
    }
}
