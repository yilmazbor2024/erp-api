using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCompanyBrand")]
    public partial class cdCompanyBrand
    {
        public cdCompanyBrand()
        {
            auOptInOptOutTraces = new HashSet<auOptInOptOutTrace>();
            cdCompanyBrandDescs = new HashSet<cdCompanyBrandDesc>();
            cdCurrAccLists = new HashSet<cdCurrAccList>();
            cdPresentCardTypes = new HashSet<cdPresentCardType>();
            dfBulkMailServiceProviderAccounts = new HashSet<dfBulkMailServiceProviderAccount>();
            dfCompanyLoyaltyPrograms = new HashSet<dfCompanyLoyaltyProgram>();
            dfMobilDevCompanyBrandCollectorIDs = new HashSet<dfMobilDevCompanyBrandCollectorID>();
            dfMobilDevStoreCompanyBrandCollectorIDs = new HashSet<dfMobilDevStoreCompanyBrandCollectorID>();
            dfOnlineSalesandPaymentParameterss = new HashSet<dfOnlineSalesandPaymentParameters>();
            dfSMSForCustomerRelationships = new HashSet<dfSMSForCustomerRelationship>();
            dfStoreDefaults = new HashSet<dfStoreDefault>();
            prCurrAccCompanyBrands = new HashSet<prCurrAccCompanyBrand>();
            prCurrAccOptInOptOutStatuss = new HashSet<prCurrAccOptInOptOutStatus>();
            prCustomerCompanyBrandAttributes = new HashSet<prCustomerCompanyBrandAttribute>();
            prInteractiveSMSApplicationss = new HashSet<prInteractiveSMSApplications>();
            prItemCompanyBrands = new HashSet<prItemCompanyBrand>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string Originator { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string OTPOriginator { get; set; }

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

        public virtual ICollection<auOptInOptOutTrace> auOptInOptOutTraces { get; set; }
        public virtual ICollection<cdCompanyBrandDesc> cdCompanyBrandDescs { get; set; }
        public virtual ICollection<cdCurrAccList> cdCurrAccLists { get; set; }
        public virtual ICollection<cdPresentCardType> cdPresentCardTypes { get; set; }
        public virtual ICollection<dfBulkMailServiceProviderAccount> dfBulkMailServiceProviderAccounts { get; set; }
        public virtual ICollection<dfCompanyLoyaltyProgram> dfCompanyLoyaltyPrograms { get; set; }
        public virtual ICollection<dfMobilDevCompanyBrandCollectorID> dfMobilDevCompanyBrandCollectorIDs { get; set; }
        public virtual ICollection<dfMobilDevStoreCompanyBrandCollectorID> dfMobilDevStoreCompanyBrandCollectorIDs { get; set; }
        public virtual ICollection<dfOnlineSalesandPaymentParameters> dfOnlineSalesandPaymentParameterss { get; set; }
        public virtual ICollection<dfSMSForCustomerRelationship> dfSMSForCustomerRelationships { get; set; }
        public virtual ICollection<dfStoreDefault> dfStoreDefaults { get; set; }
        public virtual ICollection<prCurrAccCompanyBrand> prCurrAccCompanyBrands { get; set; }
        public virtual ICollection<prCurrAccOptInOptOutStatus> prCurrAccOptInOptOutStatuss { get; set; }
        public virtual ICollection<prCustomerCompanyBrandAttribute> prCustomerCompanyBrandAttributes { get; set; }
        public virtual ICollection<prInteractiveSMSApplications> prInteractiveSMSApplicationss { get; set; }
        public virtual ICollection<prItemCompanyBrand> prItemCompanyBrands { get; set; }
    }
}
