using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDriver")]
    public partial class cdDriver
    {
        public cdDriver()
        {
            prVehicleDriverss = new HashSet<prVehicleDrivers>();
            tpInnerVehicleDriverss = new HashSet<tpInnerVehicleDrivers>();
            tpShipmentVehicleDriverss = new HashSet<tpShipmentVehicleDrivers>();
            tpVehicleLoadingDrivers = new HashSet<tpVehicleLoadingDriver>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DriverCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        public string FirstLastName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prVehicleDrivers> prVehicleDriverss { get; set; }
        public virtual ICollection<tpInnerVehicleDrivers> tpInnerVehicleDriverss { get; set; }
        public virtual ICollection<tpShipmentVehicleDrivers> tpShipmentVehicleDriverss { get; set; }
        public virtual ICollection<tpVehicleLoadingDriver> tpVehicleLoadingDrivers { get; set; }
    }
}
