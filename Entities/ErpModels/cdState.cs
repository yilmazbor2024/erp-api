using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdState")]
    public partial class cdState
    {
        public cdState()
        {
            bsTaxFreeRefundCompanys = new HashSet<bsTaxFreeRefundCompany>();
            cdCheques = new HashSet<cdCheque>();
            cdCitys = new HashSet<cdCity>();
            cdPorts = new HashSet<cdPort>();
            cdStateDescs = new HashSet<cdStateDesc>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            prBankBranchs = new HashSet<prBankBranch>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prEmployeePrevJobs = new HashSet<prEmployeePrevJob>();
            prWarehousePostalAddresss = new HashSet<prWarehousePostalAddress>();
            tpInvoicePostalAddresss = new HashSet<tpInvoicePostalAddress>();
            tpOrderPostalAddresss = new HashSet<tpOrderPostalAddress>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string StateCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

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
        public virtual cdCountry cdCountry { get; set; }

        public virtual ICollection<bsTaxFreeRefundCompany> bsTaxFreeRefundCompanys { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdCity> cdCitys { get; set; }
        public virtual ICollection<cdPort> cdPorts { get; set; }
        public virtual ICollection<cdStateDesc> cdStateDescs { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<prBankBranch> prBankBranchs { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prEmployeePrevJob> prEmployeePrevJobs { get; set; }
        public virtual ICollection<prWarehousePostalAddress> prWarehousePostalAddresss { get; set; }
        public virtual ICollection<tpInvoicePostalAddress> tpInvoicePostalAddresss { get; set; }
        public virtual ICollection<tpOrderPostalAddress> tpOrderPostalAddresss { get; set; }
    }
}
