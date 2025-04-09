using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdBatch")]
    public partial class cdBatch
    {
        public cdBatch()
        {
            cdBatchDescs = new HashSet<cdBatchDesc>();
            cdRolls = new HashSet<cdRoll>();
            prItemBatchBarcodes = new HashSet<prItemBatchBarcode>();
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            stItemRollNumbers = new HashSet<stItemRollNumber>();
            stItemRollNumberPickings = new HashSet<stItemRollNumberPicking>();
            stItemSerialNumbers = new HashSet<stItemSerialNumber>();
            tpSupportResolveMaterials = new HashSet<tpSupportResolveMaterial>();
            trCostOfGoodsSoldHeaders = new HashSet<trCostOfGoodsSoldHeader>();
            trInnerLines = new HashSet<trInnerLine>();
            trInvoiceLines = new HashSet<trInvoiceLine>();
            trPickingLines = new HashSet<trPickingLine>();
            trReportedSaleLines = new HashSet<trReportedSaleLine>();
            trShipmentLines = new HashSet<trShipmentLine>();
            trStocks = new HashSet<trStock>();
            trSupportRequestLines = new HashSet<trSupportRequestLine>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BatchCode { get; set; }

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

        public virtual ICollection<cdBatchDesc> cdBatchDescs { get; set; }
        public virtual ICollection<cdRoll> cdRolls { get; set; }
        public virtual ICollection<prItemBatchBarcode> prItemBatchBarcodes { get; set; }
        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<stItemRollNumber> stItemRollNumbers { get; set; }
        public virtual ICollection<stItemRollNumberPicking> stItemRollNumberPickings { get; set; }
        public virtual ICollection<stItemSerialNumber> stItemSerialNumbers { get; set; }
        public virtual ICollection<tpSupportResolveMaterial> tpSupportResolveMaterials { get; set; }
        public virtual ICollection<trCostOfGoodsSoldHeader> trCostOfGoodsSoldHeaders { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
        public virtual ICollection<trInvoiceLine> trInvoiceLines { get; set; }
        public virtual ICollection<trPickingLine> trPickingLines { get; set; }
        public virtual ICollection<trReportedSaleLine> trReportedSaleLines { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
        public virtual ICollection<trStock> trStocks { get; set; }
        public virtual ICollection<trSupportRequestLine> trSupportRequestLines { get; set; }
    }
}
