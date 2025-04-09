using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdPriceGroup")]
    public partial class cdPriceGroup
    {
        public cdPriceGroup()
        {
            auPriceListPermits = new HashSet<auPriceListPermit>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdLabelTypes = new HashSet<cdLabelType>();
            cdPriceGroupDescs = new HashSet<cdPriceGroupDesc>();
            dfCompanyPriceGroups = new HashSet<dfCompanyPriceGroup>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            dfStoreProductInformations = new HashSet<dfStoreProductInformation>();
            prMarketPlaceProductInformations = new HashSet<prMarketPlaceProductInformation>();
            prPosTerminalFiscalPrinters = new HashSet<prPosTerminalFiscalPrinter>();
            prRelationalPriceGroupss = new HashSet<prRelationalPriceGroups>();
            prSubCurrAccs = new HashSet<prSubCurrAcc>();
            trAgentReservationHeaders = new HashSet<trAgentReservationHeader>();
            trPriceListHeaders = new HashSet<trPriceListHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PriceGroupCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string DefaultCurrencyCode { get; set; }

        [Required]
        public bool IsTaxIncluded { get; set; }

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
        public virtual cdCurrency cdCurrency { get; set; }

        public virtual ICollection<auPriceListPermit> auPriceListPermits { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdLabelType> cdLabelTypes { get; set; }
        public virtual ICollection<cdPriceGroupDesc> cdPriceGroupDescs { get; set; }
        public virtual ICollection<dfCompanyPriceGroup> dfCompanyPriceGroups { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<dfStoreProductInformation> dfStoreProductInformations { get; set; }
        public virtual ICollection<prMarketPlaceProductInformation> prMarketPlaceProductInformations { get; set; }
        public virtual ICollection<prPosTerminalFiscalPrinter> prPosTerminalFiscalPrinters { get; set; }
        public virtual ICollection<prRelationalPriceGroups> prRelationalPriceGroupss { get; set; }
        public virtual ICollection<prSubCurrAcc> prSubCurrAccs { get; set; }
        public virtual ICollection<trAgentReservationHeader> trAgentReservationHeaders { get; set; }
        public virtual ICollection<trPriceListHeader> trPriceListHeaders { get; set; }
    }
}
