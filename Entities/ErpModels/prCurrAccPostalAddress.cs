using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccPostalAddress")]
    public partial class prCurrAccPostalAddress
    {
        public prCurrAccPostalAddress()
        {
            prCurrAccDefaults = new HashSet<prCurrAccDefault>();
            prCurrAccMapLocations = new HashSet<prCurrAccMapLocation>();
            prSubCurrAccDefaults = new HashSet<prSubCurrAccDefault>();
            rpOrderDeliveryAssignmentCollectedItemss = new HashSet<rpOrderDeliveryAssignmentCollectedItems>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
        }

        [Key]
        [Required]
        public Guid PostalAddressID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string AddressTypeCode { get; set; }

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

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string DrivingDirections { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxOfficeCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string TaxNumber { get; set; }

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

        // Navigation Properties
        public virtual cdDistrict cdDistrict { get; set; }
        public virtual cdStreet cdStreet { get; set; }
        public virtual cdTaxOffice cdTaxOffice { get; set; }
        public virtual cdAddressType cdAddressType { get; set; }
        public virtual cdState cdState { get; set; }
        public virtual cdCity cdCity { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdCountry cdCountry { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

        public virtual ICollection<prCurrAccDefault> prCurrAccDefaults { get; set; }
        public virtual ICollection<prCurrAccMapLocation> prCurrAccMapLocations { get; set; }
        public virtual ICollection<prSubCurrAccDefault> prSubCurrAccDefaults { get; set; }
        public virtual ICollection<rpOrderDeliveryAssignmentCollectedItems> rpOrderDeliveryAssignmentCollectedItemss { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
    }
}
