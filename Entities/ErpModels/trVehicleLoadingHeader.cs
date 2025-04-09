using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("trVehicleLoadingHeader")]
    public partial class trVehicleLoadingHeader
    {
        public trVehicleLoadingHeader()
        {
            tpVehicleLoadingDrivers = new HashSet<tpVehicleLoadingDriver>();
            tpVehicleLoadingRoundsmans = new HashSet<tpVehicleLoadingRoundsman>();
            trVehicleLoadingLines = new HashSet<trVehicleLoadingLine>();
        }

        [Key]
        [Required]
        public Guid VehicleLoadingHeaderID { get; set; }

        [Required]
        public object VehicleLoadingNumber { get; set; }

        [Required]
        public DateTime VehicleLoadingDate { get; set; }

        [Required]
        public TimeSpan VehicleLoadingTime { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string WarehouseCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string VehicleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DeliveryCompanyCode { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool IsPrinted { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public bool UserLocked { get; set; }

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
        public virtual cdWarehouse cdWarehouse { get; set; }
        public virtual cdVehicle cdVehicle { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdDeliveryCompany cdDeliveryCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<tpVehicleLoadingDriver> tpVehicleLoadingDrivers { get; set; }
        public virtual ICollection<tpVehicleLoadingRoundsman> tpVehicleLoadingRoundsmans { get; set; }
        public virtual ICollection<trVehicleLoadingLine> trVehicleLoadingLines { get; set; }
    }
}
