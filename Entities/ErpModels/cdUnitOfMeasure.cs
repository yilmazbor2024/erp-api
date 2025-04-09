using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdUnitOfMeasure")]
    public partial class cdUnitOfMeasure
    {
        public cdUnitOfMeasure()
        {
            bsCustomsProductGroups = new HashSet<bsCustomsProductGroup>();
            cdItems = new HashSet<cdItem>();
            cdPackageVolumes = new HashSet<cdPackageVolume>();
            cdPCTs = new HashSet<cdPCT>();
            cdUnitOfMeasureDescs = new HashSet<cdUnitOfMeasureDesc>();
            dfProductHierarchyDefaults = new HashSet<dfProductHierarchyDefault>();
            prItemCrossUnitOfMeasures = new HashSet<prItemCrossUnitOfMeasure>();
            prItemMeasuresOfVolumes = new HashSet<prItemMeasuresOfVolume>();
            tpInvoiceLinePickingDetailss = new HashSet<tpInvoiceLinePickingDetails>();
            tpShipmentLinePickingDetailss = new HashSet<tpShipmentLinePickingDetails>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trPriceListLines = new HashSet<trPriceListLine>();
            trPurchaseRequisitionLines = new HashSet<trPurchaseRequisitionLine>();
            trVendorPriceListLines = new HashSet<trVendorPriceListLine>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string UnitOfMeasureCode { get; set; }

        [Required]
        public byte RoundDigit { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MaskFormat { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InternationalUnitOfMeasureCode { get; set; }

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
        public virtual cdInternationalUnitOfMeasure cdInternationalUnitOfMeasure { get; set; }

        public virtual ICollection<bsCustomsProductGroup> bsCustomsProductGroups { get; set; }
        public virtual ICollection<cdItem> cdItems { get; set; }
        public virtual ICollection<cdPackageVolume> cdPackageVolumes { get; set; }
        public virtual ICollection<cdPCT> cdPCTs { get; set; }
        public virtual ICollection<cdUnitOfMeasureDesc> cdUnitOfMeasureDescs { get; set; }
        public virtual ICollection<dfProductHierarchyDefault> dfProductHierarchyDefaults { get; set; }
        public virtual ICollection<prItemCrossUnitOfMeasure> prItemCrossUnitOfMeasures { get; set; }
        public virtual ICollection<prItemMeasuresOfVolume> prItemMeasuresOfVolumes { get; set; }
        public virtual ICollection<tpInvoiceLinePickingDetails> tpInvoiceLinePickingDetailss { get; set; }
        public virtual ICollection<tpShipmentLinePickingDetails> tpShipmentLinePickingDetailss { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trPriceListLine> trPriceListLines { get; set; }
        public virtual ICollection<trPurchaseRequisitionLine> trPurchaseRequisitionLines { get; set; }
        public virtual ICollection<trVendorPriceListLine> trVendorPriceListLines { get; set; }
    }
}
