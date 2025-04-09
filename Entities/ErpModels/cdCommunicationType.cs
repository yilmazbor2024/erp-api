using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCommunicationType")]
    public partial class cdCommunicationType
    {
        public cdCommunicationType()
        {
            auOptInOptOutTraces = new HashSet<auOptInOptOutTrace>();
            cdCommunicationTypeDescs = new HashSet<cdCommunicationTypeDesc>();
            cdRegisteredEMailServices = new HashSet<cdRegisteredEMailService>();
            dfPDCCurrAccCommunications = new HashSet<dfPDCCurrAccCommunication>();
            hrJobInterviewResultss = new HashSet<hrJobInterviewResults>();
            lgSMSForCustomerRelationshipNonFormattedCommunicationss = new HashSet<lgSMSForCustomerRelationshipNonFormattedCommunications>();
            prConfirmationFormCommTypess = new HashSet<prConfirmationFormCommTypes>();
            prCurrAccCommunications = new HashSet<prCurrAccCommunication>();
            prProcessInfos = new HashSet<prProcessInfo>();
        }

        [Key]
        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode { get; set; }

        [Required]
        public int EditMaskCode { get; set; }

        [Required]
        public byte CommunicationKindCode { get; set; }

        [Required]
        public bool CanSendSMS { get; set; }

        [Required]
        public bool CanSendEMail { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public bool CommunicationConfirmationRequired { get; set; }

        [Required]
        public bool ConfirmCommAddressOnRepetitionOnly { get; set; }

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
        public virtual bsEditMask bsEditMask { get; set; }
        public virtual bsCommunicationKind bsCommunicationKind { get; set; }

        public virtual ICollection<auOptInOptOutTrace> auOptInOptOutTraces { get; set; }
        public virtual ICollection<cdCommunicationTypeDesc> cdCommunicationTypeDescs { get; set; }
        public virtual ICollection<cdRegisteredEMailService> cdRegisteredEMailServices { get; set; }
        public virtual ICollection<dfPDCCurrAccCommunication> dfPDCCurrAccCommunications { get; set; }
        public virtual ICollection<hrJobInterviewResults> hrJobInterviewResultss { get; set; }
        public virtual ICollection<lgSMSForCustomerRelationshipNonFormattedCommunications> lgSMSForCustomerRelationshipNonFormattedCommunicationss { get; set; }
        public virtual ICollection<prConfirmationFormCommTypes> prConfirmationFormCommTypess { get; set; }
        public virtual ICollection<prCurrAccCommunication> prCurrAccCommunications { get; set; }
        public virtual ICollection<prProcessInfo> prProcessInfos { get; set; }
    }
}
