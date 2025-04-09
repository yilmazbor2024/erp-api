using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trInnerHeader")]
    public partial class trInnerHeader
    {
        public trInnerHeader()
        {
            tpInnerHeaderExtensions = new HashSet<tpInnerHeaderExtension>();
            tpInnerTransportModeDetails = new HashSet<tpInnerTransportModeDetail>();
            tpInnerVehicleDriverss = new HashSet<tpInnerVehicleDrivers>();
            tpPickingFromSectionTransfers = new HashSet<tpPickingFromSectionTransfer>();
            tpSupportStatusHistorys = new HashSet<tpSupportStatusHistory>();
            trAdjustCostInners = new HashSet<trAdjustCostInner>();
            trInnerLines = new HashSet<trInnerLine>();
            trInnerLineSums = new HashSet<trInnerLineSum>();
            trInnerLineSumDetails = new HashSet<trInnerLineSumDetail>();
            trReserveTransfers = new HashSet<trReserveTransfer>();
        }

        [Key]
        [Required]
        public Guid InnerHeaderID { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public object InnerProcessCode { get; set; }

        [Required]
        public object InnerNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Series { get; set; }

        [Required]
        public decimal SeriesNumber { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ToStoreCode { get; set; }

        [Required]
        public object ToOfficeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ToWarehouseCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ServicemanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CustomsDocumentNumber { get; set; }

        [Required]
        public bool IsInnerOrderBase { get; set; }

        [Required]
        public bool IsSectionTransfer { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsTransferApproved { get; set; }

        [Required]
        public DateTime TransferApprovedDate { get; set; }

        [Required]
        public bool IsPostingJournal { get; set; }

        [Required]
        public DateTime JournalDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        public Guid? ApplicationID { get; set; }

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

        // Navigation Properties
        public virtual bsTransType bsTransType { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdServiceman cdServiceman { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual bsInnerProcess bsInnerProcess { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpInnerHeaderExtension> tpInnerHeaderExtensions { get; set; }
        public virtual ICollection<tpInnerTransportModeDetail> tpInnerTransportModeDetails { get; set; }
        public virtual ICollection<tpInnerVehicleDrivers> tpInnerVehicleDriverss { get; set; }
        public virtual ICollection<tpPickingFromSectionTransfer> tpPickingFromSectionTransfers { get; set; }
        public virtual ICollection<tpSupportStatusHistory> tpSupportStatusHistorys { get; set; }
        public virtual ICollection<trAdjustCostInner> trAdjustCostInners { get; set; }
        public virtual ICollection<trInnerLine> trInnerLines { get; set; }
        public virtual ICollection<trInnerLineSum> trInnerLineSums { get; set; }
        public virtual ICollection<trInnerLineSumDetail> trInnerLineSumDetails { get; set; }
        public virtual ICollection<trReserveTransfer> trReserveTransfers { get; set; }
    }
}
