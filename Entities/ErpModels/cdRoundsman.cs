using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdRoundsman")]
    public partial class cdRoundsman
    {
        public cdRoundsman()
        {
            prRoundsmanResponsibilityAreas = new HashSet<prRoundsmanResponsibilityArea>();
            tpInnerHeaderExtensions = new HashSet<tpInnerHeaderExtension>();
            tpSupportResolves = new HashSet<tpSupportResolve>();
            tpVehicleLoadingLineDeliveryStatuss = new HashSet<tpVehicleLoadingLineDeliveryStatus>();
            tpVehicleLoadingRoundsmans = new HashSet<tpVehicleLoadingRoundsman>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
            trInnerHeaders = new HashSet<trInnerHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string RoundsmanCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FirstLastName { get; set; }

        [Required]
        public bool SignOff { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string VehiclePlateNum { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte EmployeeTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string EmployeeCode { get; set; }

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
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prRoundsmanResponsibilityArea> prRoundsmanResponsibilityAreas { get; set; }
        public virtual ICollection<tpInnerHeaderExtension> tpInnerHeaderExtensions { get; set; }
        public virtual ICollection<tpSupportResolve> tpSupportResolves { get; set; }
        public virtual ICollection<tpVehicleLoadingLineDeliveryStatus> tpVehicleLoadingLineDeliveryStatuss { get; set; }
        public virtual ICollection<tpVehicleLoadingRoundsman> tpVehicleLoadingRoundsmans { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
        public virtual ICollection<trInnerHeader> trInnerHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
    }
}
