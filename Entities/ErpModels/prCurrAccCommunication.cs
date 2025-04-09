using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccCommunication")]
    public partial class prCurrAccCommunication
    {
        public prCurrAccCommunication()
        {
            prCurrAccCommunicationEncs = new HashSet<prCurrAccCommunicationEnc>();
            prCurrAccCommunicationFormatteds = new HashSet<prCurrAccCommunicationFormatted>();
            prCurrAccDefaults = new HashSet<prCurrAccDefault>();
            prCurrAccOptInOptOutStatuss = new HashSet<prCurrAccOptInOptOutStatus>();
            prCurrAccReconciliationContacts = new HashSet<prCurrAccReconciliationContact>();
            prSubCurrAccDefaults = new HashSet<prSubCurrAccDefault>();
        }

        [Key]
        [Required]
        public Guid CommunicationID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CommAddress { get; set; }

        [Required]
        public bool CanSendAdvert { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FormNumber { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

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

        // Navigation Properties
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdCommunicationType cdCommunicationType { get; set; }

        public virtual ICollection<prCurrAccCommunicationEnc> prCurrAccCommunicationEncs { get; set; }
        public virtual ICollection<prCurrAccCommunicationFormatted> prCurrAccCommunicationFormatteds { get; set; }
        public virtual ICollection<prCurrAccDefault> prCurrAccDefaults { get; set; }
        public virtual ICollection<prCurrAccOptInOptOutStatus> prCurrAccOptInOptOutStatuss { get; set; }
        public virtual ICollection<prCurrAccReconciliationContact> prCurrAccReconciliationContacts { get; set; }
        public virtual ICollection<prSubCurrAccDefault> prSubCurrAccDefaults { get; set; }
    }
}
