using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdLot")]
    public partial class cdLot
    {
        public cdLot()
        {
            cdLotDescs = new HashSet<cdLotDesc>();
            prCurrAccLotGrAtts = new HashSet<prCurrAccLotGrAtt>();
            prLinkedProductContentSums = new HashSet<prLinkedProductContentSum>();
            prLinkedProductContentSumDetails = new HashSet<prLinkedProductContentSumDetail>();
            prLotQtys = new HashSet<prLotQty>();
            prProductLots = new HashSet<prProductLot>();
            prProductLotBarcodes = new HashSet<prProductLotBarcode>();
            trAllocationProductQtys = new HashSet<trAllocationProductQty>();
            trInnerLineSums = new HashSet<trInnerLineSum>();
            trInnerLineSumDetails = new HashSet<trInnerLineSumDetail>();
            trInnerOrderLineSums = new HashSet<trInnerOrderLineSum>();
            trInnerOrderLineSumDetails = new HashSet<trInnerOrderLineSumDetail>();
            trInvoiceLineSums = new HashSet<trInvoiceLineSum>();
            trInvoiceLineSumDetails = new HashSet<trInvoiceLineSumDetail>();
            trOrderAsnLineSums = new HashSet<trOrderAsnLineSum>();
            trOrderAsnLineSumDetails = new HashSet<trOrderAsnLineSumDetail>();
            trOrderLineSums = new HashSet<trOrderLineSum>();
            trOrderLineSumDetails = new HashSet<trOrderLineSumDetail>();
            trProposalLineSums = new HashSet<trProposalLineSum>();
            trProposalLineSumDetails = new HashSet<trProposalLineSumDetail>();
            trSalesPlanProductQtys = new HashSet<trSalesPlanProductQty>();
            trShipmentLineSums = new HashSet<trShipmentLineSum>();
            trShipmentLineSumDetails = new HashSet<trShipmentLineSumDetail>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string LotCode { get; set; }

        [Required]
        public byte ItemDimTypeCode { get; set; }

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
        public virtual bsItemDimType bsItemDimType { get; set; }

        public virtual ICollection<cdLotDesc> cdLotDescs { get; set; }
        public virtual ICollection<prCurrAccLotGrAtt> prCurrAccLotGrAtts { get; set; }
        public virtual ICollection<prLinkedProductContentSum> prLinkedProductContentSums { get; set; }
        public virtual ICollection<prLinkedProductContentSumDetail> prLinkedProductContentSumDetails { get; set; }
        public virtual ICollection<prLotQty> prLotQtys { get; set; }
        public virtual ICollection<prProductLot> prProductLots { get; set; }
        public virtual ICollection<prProductLotBarcode> prProductLotBarcodes { get; set; }
        public virtual ICollection<trAllocationProductQty> trAllocationProductQtys { get; set; }
        public virtual ICollection<trInnerLineSum> trInnerLineSums { get; set; }
        public virtual ICollection<trInnerLineSumDetail> trInnerLineSumDetails { get; set; }
        public virtual ICollection<trInnerOrderLineSum> trInnerOrderLineSums { get; set; }
        public virtual ICollection<trInnerOrderLineSumDetail> trInnerOrderLineSumDetails { get; set; }
        public virtual ICollection<trInvoiceLineSum> trInvoiceLineSums { get; set; }
        public virtual ICollection<trInvoiceLineSumDetail> trInvoiceLineSumDetails { get; set; }
        public virtual ICollection<trOrderAsnLineSum> trOrderAsnLineSums { get; set; }
        public virtual ICollection<trOrderAsnLineSumDetail> trOrderAsnLineSumDetails { get; set; }
        public virtual ICollection<trOrderLineSum> trOrderLineSums { get; set; }
        public virtual ICollection<trOrderLineSumDetail> trOrderLineSumDetails { get; set; }
        public virtual ICollection<trProposalLineSum> trProposalLineSums { get; set; }
        public virtual ICollection<trProposalLineSumDetail> trProposalLineSumDetails { get; set; }
        public virtual ICollection<trSalesPlanProductQty> trSalesPlanProductQtys { get; set; }
        public virtual ICollection<trShipmentLineSum> trShipmentLineSums { get; set; }
        public virtual ICollection<trShipmentLineSumDetail> trShipmentLineSumDetails { get; set; }
    }
}
