using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("tpOrderPostalAddress")]
    public partial class tpOrderPostalAddress
    {
        public tpOrderPostalAddress()
        {
        }

        [Key]
        [Required]
        public Guid OrderHeaderID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        public string FirstLastName { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string CompanyName { get; set; }

        [Required]
        public long AddressID { get; set; }

        [Required]
        [StringLength(10002000)]
        [Column(TypeName = "Char1000(2000)")]
        public object Address { get; set; }

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

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

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
        public virtual cdCity cdCity { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual cdStreet cdStreet { get; set; }
        public virtual cdDistrict cdDistrict { get; set; }

    }
}
