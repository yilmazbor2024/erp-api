using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCountry")]
    public partial class cdCountry
    {
        public cdCountry()
        {
            bsTaxFreeRefundCompanys = new HashSet<bsTaxFreeRefundCompany>();
            cdCheques = new HashSet<cdCheque>();
            cdCountryDescs = new HashSet<cdCountryDesc>();
            cdLabelTypes = new HashSet<cdLabelType>();
            cdPorts = new HashSet<cdPort>();
            cdStates = new HashSet<cdState>();
            cdUniversitys = new HashSet<cdUniversity>();
            cdWorkPlaces = new HashSet<cdWorkPlace>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            dfTaxFreeRefundRates = new HashSet<dfTaxFreeRefundRate>();
            prBankBranchs = new HashSet<prBankBranch>();
            prCountryCallingCodes = new HashSet<prCountryCallingCode>();
            prCountryPCTApplicablePaymentTypess = new HashSet<prCountryPCTApplicablePaymentTypes>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prEmployeePrevJobs = new HashSet<prEmployeePrevJob>();
            prExportFileShippingInfos = new HashSet<prExportFileShippingInfo>();
            prImportFileShippingInfos = new HashSet<prImportFileShippingInfo>();
            prItemBasePrices = new HashSet<prItemBasePrice>();
            prItemDim1Equs = new HashSet<prItemDim1Equ>();
            prItemDim2Equs = new HashSet<prItemDim2Equ>();
            prItemDim3Equs = new HashSet<prItemDim3Equ>();
            prItemTaxGrAtts = new HashSet<prItemTaxGrAtt>();
            prItemTextileCareSymbols = new HashSet<prItemTextileCareSymbol>();
            prMedicalProductImportCountriess = new HashSet<prMedicalProductImportCountries>();
            prMedicalProductOriginCountriess = new HashSet<prMedicalProductOriginCountries>();
            prWarehousePostalAddresss = new HashSet<prWarehousePostalAddress>();
            tpInvoicePostalAddresss = new HashSet<tpInvoicePostalAddress>();
            tpOrderPostalAddresss = new HashSet<tpOrderPostalAddress>();
            trCountrySpecialDays = new HashSet<trCountrySpecialDay>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CurrencyCode { get; set; }

        [Required]
        public bool UseVat { get; set; }

        [Required]
        public bool IsVatRequired { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TaxDecCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CountryISOCode { get; set; }

        [Required]
        public bool UseItemDim1Equ { get; set; }

        [Required]
        public bool IsItemDim1Required { get; set; }

        [Required]
        public bool UseItemDim2Equ { get; set; }

        [Required]
        public bool IsItemDim2Required { get; set; }

        [Required]
        public bool UseItemDim3Equ { get; set; }

        [Required]
        public bool IsItemDim3Required { get; set; }

        [Required]
        public bool ApplyPCTOnSelectedPaymentTypes { get; set; }

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
        public virtual bsCountryISO bsCountryISO { get; set; }

        public virtual ICollection<bsTaxFreeRefundCompany> bsTaxFreeRefundCompanys { get; set; }
        public virtual ICollection<cdCheque> cdCheques { get; set; }
        public virtual ICollection<cdCountryDesc> cdCountryDescs { get; set; }
        public virtual ICollection<cdLabelType> cdLabelTypes { get; set; }
        public virtual ICollection<cdPort> cdPorts { get; set; }
        public virtual ICollection<cdState> cdStates { get; set; }
        public virtual ICollection<cdUniversity> cdUniversitys { get; set; }
        public virtual ICollection<cdWorkPlace> cdWorkPlaces { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<dfTaxFreeRefundRate> dfTaxFreeRefundRates { get; set; }
        public virtual ICollection<prBankBranch> prBankBranchs { get; set; }
        public virtual ICollection<prCountryCallingCode> prCountryCallingCodes { get; set; }
        public virtual ICollection<prCountryPCTApplicablePaymentTypes> prCountryPCTApplicablePaymentTypess { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prEmployeePrevJob> prEmployeePrevJobs { get; set; }
        public virtual ICollection<prExportFileShippingInfo> prExportFileShippingInfos { get; set; }
        public virtual ICollection<prImportFileShippingInfo> prImportFileShippingInfos { get; set; }
        public virtual ICollection<prItemBasePrice> prItemBasePrices { get; set; }
        public virtual ICollection<prItemDim1Equ> prItemDim1Equs { get; set; }
        public virtual ICollection<prItemDim2Equ> prItemDim2Equs { get; set; }
        public virtual ICollection<prItemDim3Equ> prItemDim3Equs { get; set; }
        public virtual ICollection<prItemTaxGrAtt> prItemTaxGrAtts { get; set; }
        public virtual ICollection<prItemTextileCareSymbol> prItemTextileCareSymbols { get; set; }
        public virtual ICollection<prMedicalProductImportCountries> prMedicalProductImportCountriess { get; set; }
        public virtual ICollection<prMedicalProductOriginCountries> prMedicalProductOriginCountriess { get; set; }
        public virtual ICollection<prWarehousePostalAddress> prWarehousePostalAddresss { get; set; }
        public virtual ICollection<tpInvoicePostalAddress> tpInvoicePostalAddresss { get; set; }
        public virtual ICollection<tpOrderPostalAddress> tpOrderPostalAddresss { get; set; }
        public virtual ICollection<trCountrySpecialDay> trCountrySpecialDays { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
    }
}
