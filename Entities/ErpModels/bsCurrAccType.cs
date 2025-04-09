using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("bsCurrAccType")]
    public partial class bsCurrAccType
    {
        public bsCurrAccType()
        {
            bsCurrAccTypeDescs = new HashSet<bsCurrAccTypeDesc>();
            bsReconciliationTypes = new HashSet<bsReconciliationType>();
            cdCurrAccs = new HashSet<cdCurrAcc>();
            cdCurrAccAttributeTypes = new HashSet<cdCurrAccAttributeType>();
            cdDiscountOffers = new HashSet<cdDiscountOffer>();
            cdSubCurrAccAttributeTypes = new HashSet<cdSubCurrAccAttributeType>();
            cdSurveys = new HashSet<cdSurvey>();
            dfCompanyCurrAccSizes = new HashSet<dfCompanyCurrAccSize>();
            dfPDCCurrAccs = new HashSet<dfPDCCurrAcc>();
            dfPDCCurrAccCommunications = new HashSet<dfPDCCurrAccCommunication>();
            dfPDCCurrAccContacts = new HashSet<dfPDCCurrAccContact>();
            dfPDCCurrAccPersonalInfos = new HashSet<dfPDCCurrAccPersonalInfo>();
            dfPDCCurrAccPostalAddresss = new HashSet<dfPDCCurrAccPostalAddress>();
            dfPDCElementss = new HashSet<dfPDCElements>();
            dfSMSForCustomerRelationships = new HashSet<dfSMSForCustomerRelationship>();
            dfSupportRequestSurveyDefaults = new HashSet<dfSupportRequestSurveyDefault>();
            prPersonalDataConfirmationFormTypeForCurrAccTypess = new HashSet<prPersonalDataConfirmationFormTypeForCurrAccTypes>();
            srCodeNumberCurrAccs = new HashSet<srCodeNumberCurrAcc>();
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        public virtual ICollection<bsCurrAccTypeDesc> bsCurrAccTypeDescs { get; set; }
        public virtual ICollection<bsReconciliationType> bsReconciliationTypes { get; set; }
        public virtual ICollection<cdCurrAcc> cdCurrAccs { get; set; }
        public virtual ICollection<cdCurrAccAttributeType> cdCurrAccAttributeTypes { get; set; }
        public virtual ICollection<cdDiscountOffer> cdDiscountOffers { get; set; }
        public virtual ICollection<cdSubCurrAccAttributeType> cdSubCurrAccAttributeTypes { get; set; }
        public virtual ICollection<cdSurvey> cdSurveys { get; set; }
        public virtual ICollection<dfCompanyCurrAccSize> dfCompanyCurrAccSizes { get; set; }
        public virtual ICollection<dfPDCCurrAcc> dfPDCCurrAccs { get; set; }
        public virtual ICollection<dfPDCCurrAccCommunication> dfPDCCurrAccCommunications { get; set; }
        public virtual ICollection<dfPDCCurrAccContact> dfPDCCurrAccContacts { get; set; }
        public virtual ICollection<dfPDCCurrAccPersonalInfo> dfPDCCurrAccPersonalInfos { get; set; }
        public virtual ICollection<dfPDCCurrAccPostalAddress> dfPDCCurrAccPostalAddresss { get; set; }
        public virtual ICollection<dfPDCElements> dfPDCElementss { get; set; }
        public virtual ICollection<dfSMSForCustomerRelationship> dfSMSForCustomerRelationships { get; set; }
        public virtual ICollection<dfSupportRequestSurveyDefault> dfSupportRequestSurveyDefaults { get; set; }
        public virtual ICollection<prPersonalDataConfirmationFormTypeForCurrAccTypes> prPersonalDataConfirmationFormTypeForCurrAccTypess { get; set; }
        public virtual ICollection<srCodeNumberCurrAcc> srCodeNumberCurrAccs { get; set; }
    }
}
