using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdShipmentMethod")]
    public partial class cdShipmentMethod
    {
        public cdShipmentMethod()
        {
            cdShipmentMethodDescs = new HashSet<cdShipmentMethodDesc>();
            dfStoreDefaults = new HashSet<dfStoreDefault>();
            prExportFileShippingInfos = new HashSet<prExportFileShippingInfo>();
            prImportFileShippingInfos = new HashSet<prImportFileShippingInfo>();
            tpInnerHeaderExtensions = new HashSet<tpInnerHeaderExtension>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string TransportModeCode { get; set; }

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
        public virtual bsTransportMode bsTransportMode { get; set; }

        public virtual ICollection<cdShipmentMethodDesc> cdShipmentMethodDescs { get; set; }
        public virtual ICollection<dfStoreDefault> dfStoreDefaults { get; set; }
        public virtual ICollection<prExportFileShippingInfo> prExportFileShippingInfos { get; set; }
        public virtual ICollection<prImportFileShippingInfo> prImportFileShippingInfos { get; set; }
        public virtual ICollection<tpInnerHeaderExtension> tpInnerHeaderExtensions { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
    }
}
