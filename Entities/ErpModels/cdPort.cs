using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPort")]
    public partial class cdPort
    {
        public cdPort()
        {
            cdPortDescs = new HashSet<cdPortDesc>();
            prExportFileShippingInfos = new HashSet<prExportFileShippingInfo>();
            prImportFileShippingInfos = new HashSet<prImportFileShippingInfo>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PortCode { get; set; }

        [Required]
        public long AddressID { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Address { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string SiteName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string BuildingName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string BuildingNum { get; set; }

        [Required]
        public short FloorNum { get; set; }

        [Required]
        public short DoorNum { get; set; }

        [Required]
        public int QuarterCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string QuarterName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Boulevard { get; set; }

        [Required]
        public int StreetCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Street { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Road { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }

   

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string ZipCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DrivingDirections { get; set; }

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
        public virtual cdCity cdCity { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdStreet cdStreet { get; set; }

        public virtual ICollection<cdPortDesc> cdPortDescs { get; set; }
        public virtual ICollection<prExportFileShippingInfo> prExportFileShippingInfos { get; set; }
        public virtual ICollection<prImportFileShippingInfo> prImportFileShippingInfos { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
    }
}
