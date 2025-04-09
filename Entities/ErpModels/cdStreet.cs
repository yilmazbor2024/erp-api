using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdStreet")]
    public partial class cdStreet
    {
        public cdStreet()
        {
            bsTaxFreeRefundCompanys = new HashSet<bsTaxFreeRefundCompany>();
            cdPorts = new HashSet<cdPort>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prResponsibilityAreaPostalAddresss = new HashSet<prResponsibilityAreaPostalAddress>();
            prWarehousePostalAddresss = new HashSet<prWarehousePostalAddress>();
            tpInvoicePostalAddresss = new HashSet<tpInvoicePostalAddress>();
            tpOrderPostalAddresss = new HashSet<tpOrderPostalAddress>();
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string DistrictCode { get; set; }

        [Key]
        [Required]
        public int QuarterCode { get; set; }

        [Key]
        [Required]
        public int StreetCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string Street { get; set; }

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
        public virtual cdQuarter cdQuarter { get; set; }

        public virtual ICollection<bsTaxFreeRefundCompany> bsTaxFreeRefundCompanys { get; set; }
        public virtual ICollection<cdPort> cdPorts { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prResponsibilityAreaPostalAddress> prResponsibilityAreaPostalAddresss { get; set; }
        public virtual ICollection<prWarehousePostalAddress> prWarehousePostalAddresss { get; set; }
        public virtual ICollection<tpInvoicePostalAddress> tpInvoicePostalAddresss { get; set; }
        public virtual ICollection<tpOrderPostalAddress> tpOrderPostalAddresss { get; set; }
    }
}
