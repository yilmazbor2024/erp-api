using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCity")]
    public partial class cdCity
    {
        public cdCity()
        {
            bsTaxFreeRefundCompanys = new HashSet<bsTaxFreeRefundCompany>();
            cdCheques = new HashSet<cdCheque>();
            cdCityDescs = new HashSet<cdCityDesc>();
            cdDistricts = new HashSet<cdDistrict>();
            cdExecutionOffices = new HashSet<cdExecutionOffice>();
            cdPorts = new HashSet<cdPort>();
            cdTaxOffices = new HashSet<cdTaxOffice>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            prBankBranchs = new HashSet<prBankBranch>();
            prCityMapLocations = new HashSet<prCityMapLocation>();
            prCurrAccPersonalInfos = new HashSet<prCurrAccPersonalInfo>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prEmployeePrevJobs = new HashSet<prEmployeePrevJob>();
            prResponsibilityAreaPostalAddresss = new HashSet<prResponsibilityAreaPostalAddress>();
            prWarehousePostalAddresss = new HashSet<prWarehousePostalAddress>();
            tpInvoiceadditionalDeliveryProcessesDistances = new HashSet<tpInvoiceadditionalDeliveryProcessesDistance>();
            tpInvoicePostalAddresss = new HashSet<tpInvoicePostalAddress>();
            tpOrderPostalAddresss = new HashSet<tpOrderPostalAddress>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CityCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StateCode { get; set; }

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
        public virtual cdState cdState { get; set; }

        public virtual ICollection<bsTaxFreeRefundCompany> bsTaxFreeRefundCompanys { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdCityDesc> cdCityDescs { get; set; }
        public virtual ICollection<cdDistrict> cdDistricts { get; set; }
        public virtual ICollection<cdExecutionOffice> cdExecutionOffices { get; set; }
        public virtual ICollection<cdPort> cdPorts { get; set; }
        public virtual ICollection<cdTaxOffice> cdTaxOffices { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<prBankBranch> prBankBranchs { get; set; }
        public virtual ICollection<prCityMapLocation> prCityMapLocations { get; set; }
        public virtual ICollection<prCurrAccPersonalInfo> prCurrAccPersonalInfos { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prEmployeePrevJob> prEmployeePrevJobs { get; set; }
        public virtual ICollection<prResponsibilityAreaPostalAddress> prResponsibilityAreaPostalAddresss { get; set; }
        public virtual ICollection<prWarehousePostalAddress> prWarehousePostalAddresss { get; set; }
        public virtual ICollection<tpInvoiceadditionalDeliveryProcessesDistance> tpInvoiceadditionalDeliveryProcessesDistances { get; set; }
        public virtual ICollection<tpInvoicePostalAddress> tpInvoicePostalAddresss { get; set; }
        public virtual ICollection<tpOrderPostalAddress> tpOrderPostalAddresss { get; set; }
    }
}
