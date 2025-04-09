using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trShipmentHeader")]
    public partial class trShipmentHeader
    {
        public trShipmentHeader()
        {
            tpShipmentHeaderExtensions = new HashSet<tpShipmentHeaderExtension>();
            tpShipmentLinePickingDetailss = new HashSet<tpShipmentLinePickingDetails>();
            tpShipmentTransportModeDetails = new HashSet<tpShipmentTransportModeDetail>();
            tpShipmentUBLExtensionss = new HashSet<tpShipmentUBLExtensions>();
            tpShipmentVehicleDriverss = new HashSet<tpShipmentVehicleDrivers>();
            trShipmentLines = new HashSet<trShipmentLine>();
            trShipmentLineSums = new HashSet<trShipmentLineSum>();
            trShipmentLineSumDetails = new HashSet<trShipmentLineSumDetail>();
            trVehicleLoadingLines = new HashSet<trVehicleLoadingLine>();
        }

        [Key]
        [Required]
        public Guid ShipmentHeaderID { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object ShippingNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime ShippingDate { get; set; }

        [Required]
        public TimeSpan ShippingTime { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        public TimeSpan OperationTime { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string Series { get; set; }

        [Required]
        public decimal SeriesNumber { get; set; }

        public string Description { get; set; }

        public string InternalDescription { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        public Guid? ShippingPostalAddressID { get; set; }

        public Guid? BillingPostalAddressID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LogisticsCompanyBOL { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CustomerASNNumber { get; set; }

        [Required]
        public object CompanyCode { get; set; }

    

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ToWarehouseCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ExportFileNumber { get; set; }

        [Required]
        public bool IsOrderBase { get; set; }

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

        public bool? IsPostingJournal { get; set; }

        public DateTime? JournalDate { get; set; }

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
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdRoundsman cdRoundsman { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual bsTransType bsTransType { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual cdExportFile cdExportFile { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<tpShipmentHeaderExtension> tpShipmentHeaderExtensions { get; set; }
        public virtual ICollection<tpShipmentLinePickingDetails> tpShipmentLinePickingDetailss { get; set; }
        public virtual ICollection<tpShipmentTransportModeDetail> tpShipmentTransportModeDetails { get; set; }
        public virtual ICollection<tpShipmentUBLExtensions> tpShipmentUBLExtensionss { get; set; }
        public virtual ICollection<tpShipmentVehicleDrivers> tpShipmentVehicleDriverss { get; set; }
        public virtual ICollection<trShipmentLine> trShipmentLines { get; set; }
        public virtual ICollection<trShipmentLineSum> trShipmentLineSums { get; set; }
        public virtual ICollection<trShipmentLineSumDetail> trShipmentLineSumDetails { get; set; }
        public virtual ICollection<trVehicleLoadingLine> trVehicleLoadingLines { get; set; }
    }
}
