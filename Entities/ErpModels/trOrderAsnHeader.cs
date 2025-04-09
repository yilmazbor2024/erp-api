using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trOrderAsnHeader")]
    public partial class trOrderAsnHeader
    {
        public trOrderAsnHeader()
        {
            trOrderAsnLines = new HashSet<trOrderAsnLine>();
            trOrderAsnLineSums = new HashSet<trOrderAsnLineSum>();
            trOrderAsnLineSumDetails = new HashSet<trOrderAsnLineSumDetail>();
        }

        [Key]
        [Required]
        public Guid OrderAsnHeaderID { get; set; }

        [Required]
        public byte TransTypeCode { get; set; }

        [Required]
        public object ProcessCode { get; set; }

        [Required]
        public object OrderAsnNumber { get; set; }

        [Required]
        public bool IsReturn { get; set; }

        [Required]
        public DateTime OrderAsnDate { get; set; }

        [Required]
        public TimeSpan OrderAsnTime { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ShipmentMethodCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string NameOfShip { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string DischargePortCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PortOfDischarge { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode1 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string IncotermCode2 { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string LettersOfCreditNumber { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime DateOfLading { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LadingPortCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string PortOfLading { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string BillOfLadingNumber { get; set; }

        [Required]
        public int TotalPackage { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ContainerNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContainerTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DepartureCountryCode { get; set; }

        [Required]
        public double GrossWeight { get; set; }

        [Required]
        public double NetWeight { get; set; }

        [Required]
        public double TotalCBM { get; set; }

        [Required]
        public double TotalCHW { get; set; }

        [Required]
        public DateTime EstimatedDateOfArrival { get; set; }

        [Required]
        public DateTime ActualDateOfArrival { get; set; }

        [Required]
        public DateTime DateOfNationalization { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

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

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsTransferApproved { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

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
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdPort cdPort { get; set; }
        public virtual bsIncoterm bsIncoterm { get; set; }
        public virtual cdContainerType cdContainerType { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual bsProcess bsProcess { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual bsTransType bsTransType { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<trOrderAsnLine> trOrderAsnLines { get; set; }
        public virtual ICollection<trOrderAsnLineSum> trOrderAsnLineSums { get; set; }
        public virtual ICollection<trOrderAsnLineSumDetail> trOrderAsnLineSumDetails { get; set; }
    }
}
