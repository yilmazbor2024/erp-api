using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prImportFileShippingInfo")]
    public partial class prImportFileShippingInfo
    {
        public prImportFileShippingInfo()
        {
        }

        [Key]
        [Required]
        public Guid ImportFileShippingInfoID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ImportFileNumber { get; set; }

        [Required]
        public DateTime DateOfLading { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DocumentNumber { get; set; }

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

        [Required]
        public DateTime EstimatedDateOfArrival { get; set; }

        [Required]
        public DateTime ActualDateOfArrival { get; set; }

        [Required]
        public DateTime DateOfNationalization { get; set; }

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
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdShipmentMethod cdShipmentMethod { get; set; }
        public virtual cdImportFile cdImportFile { get; set; }
        public virtual bsIncoterm bsIncoterm { get; set; }
        public virtual cdContainerType cdContainerType { get; set; }
        public virtual cdPort cdPort { get; set; }

    }
}
