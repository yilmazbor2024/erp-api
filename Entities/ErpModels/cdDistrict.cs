using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdDistrict")]
    public partial class cdDistrict
    {
        public cdDistrict()
        {
            bsTaxFreeRefundCompanys = new HashSet<bsTaxFreeRefundCompany>();
            cdCheques = new HashSet<cdCheque>();
            cdDistrictDescs = new HashSet<cdDistrictDesc>();
            cdPorts = new HashSet<cdPort>();
            cdQuarters = new HashSet<cdQuarter>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            prBankBranchs = new HashSet<prBankBranch>();
            prCurrAccPersonalInfos = new HashSet<prCurrAccPersonalInfo>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prDistrictMapLocations = new HashSet<prDistrictMapLocation>();
            prWarehousePostalAddresss = new HashSet<prWarehousePostalAddress>();
            tpInvoiceadditionalDeliveryProcessesDistances = new HashSet<tpInvoiceadditionalDeliveryProcessesDistance>();
            tpInvoicePostalAddresss = new HashSet<tpInvoicePostalAddress>();
            tpOrderPostalAddresss = new HashSet<tpOrderPostalAddress>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

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

        public virtual ICollection<bsTaxFreeRefundCompany> bsTaxFreeRefundCompanys { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdDistrictDesc> cdDistrictDescs { get; set; }
        public virtual ICollection<cdPort> cdPorts { get; set; }
        public virtual ICollection<cdQuarter> cdQuarters { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<prBankBranch> prBankBranchs { get; set; }
        public virtual ICollection<prCurrAccPersonalInfo> prCurrAccPersonalInfos { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prDistrictMapLocation> prDistrictMapLocations { get; set; }
        public virtual ICollection<prWarehousePostalAddress> prWarehousePostalAddresss { get; set; }
        public virtual ICollection<tpInvoiceadditionalDeliveryProcessesDistance> tpInvoiceadditionalDeliveryProcessesDistances { get; set; }
        public virtual ICollection<tpInvoicePostalAddress> tpInvoicePostalAddresss { get; set; }
        public virtual ICollection<tpOrderPostalAddress> tpOrderPostalAddresss { get; set; }
    }
}
