using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdConfirmationFormType")]
    public partial class cdConfirmationFormType
    {
        public cdConfirmationFormType()
        {
            auOptInOptOutTraces = new HashSet<auOptInOptOutTrace>();
            cdConfirmationFormTypeDescs = new HashSet<cdConfirmationFormTypeDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            dfOfficeDefaults = new HashSet<dfOfficeDefault>();
            prConfirmationFormCommTypess = new HashSet<prConfirmationFormCommTypes>();
            prConfirmationFormContents = new HashSet<prConfirmationFormContent>();
            prCurrAccOptInOptOutStatuss = new HashSet<prCurrAccOptInOptOutStatus>();
            prCurrAccPersonalDataConfirmations = new HashSet<prCurrAccPersonalDataConfirmation>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
            prPersonalDataConfirmationFormTypeForCurrAccTypess = new HashSet<prPersonalDataConfirmationFormTypeForCurrAccTypes>();
            prPresentCardActivationStepss = new HashSet<prPresentCardActivationSteps>();
            srRefNumberConfirmationForms = new HashSet<srRefNumberConfirmationForm>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FormName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string ConsentSource { get; set; }

        [Required]
        public bool UseForCommunication { get; set; }

        [Required]
        public bool UseForPresentCard { get; set; }

        [Required]
        public bool UsePreprintedForm { get; set; }

        [Required]
        public bool UseSameCardNumberWithFormNumber { get; set; }

        [Required]
        public bool UseForPersonalData { get; set; }

        [Required]
        public bool DoNotRequestReConfirmation { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string FormNumberPrefix { get; set; }

        [Required]
        public bool UseSMS { get; set; }

        [Required]
        public bool UsePreConfiguredOptions { get; set; }

        [Required]
        public bool DataProcessPermission { get; set; }

        [Required]
        public bool CanShareWithThirdParty { get; set; }

        [Required]
        public bool CanShareWithForeignCountries { get; set; }

        [Required]
        public bool CallPermission { get; set; }

        [Required]
        public bool SmsPermission { get; set; }

        [Required]
        public bool EmailPermission { get; set; }

        [Required]
        public bool AddressPermission { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormStatusCode { get; set; }

        [Required]
        public bool UseSMSWithCustomerInformation { get; set; }

        [Required]
        public bool OptOutCommunicationsWithPDCInactivation { get; set; }

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
        public virtual bsConsentSource bsConsentSource { get; set; }
        public virtual cdConfirmationFormStatus cdConfirmationFormStatus { get; set; }

        public virtual ICollection<auOptInOptOutTrace> auOptInOptOutTraces { get; set; }
        public virtual ICollection<cdConfirmationFormTypeDesc> cdConfirmationFormTypeDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<dfOfficeDefault> dfOfficeDefaults { get; set; }
        public virtual ICollection<prConfirmationFormCommTypes> prConfirmationFormCommTypess { get; set; }
        public virtual ICollection<prConfirmationFormContent> prConfirmationFormContents { get; set; }
        public virtual ICollection<prCurrAccOptInOptOutStatus> prCurrAccOptInOptOutStatuss { get; set; }
        public virtual ICollection<prCurrAccPersonalDataConfirmation> prCurrAccPersonalDataConfirmations { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
        public virtual ICollection<prPersonalDataConfirmationFormTypeForCurrAccTypes> prPersonalDataConfirmationFormTypeForCurrAccTypess { get; set; }
        public virtual ICollection<prPresentCardActivationSteps> prPresentCardActivationStepss { get; set; }
        public virtual ICollection<srRefNumberConfirmationForm> srRefNumberConfirmationForms { get; set; }
    }
}
