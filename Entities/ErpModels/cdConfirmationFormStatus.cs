using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdConfirmationFormStatus")]
    public partial class cdConfirmationFormStatus
    {
        public cdConfirmationFormStatus()
        {
            cdConfirmationFormStatusDescs = new HashSet<cdConfirmationFormStatusDesc>();
            cdConfirmationFormTypes = new HashSet<cdConfirmationFormType>();
            prCurrAccOptInOptOutStatuss = new HashSet<prCurrAccOptInOptOutStatus>();
            prCurrAccPersonalDataConfirmations = new HashSet<prCurrAccPersonalDataConfirmation>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormStatusCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InactivationReasonCode { get; set; }

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

        public virtual ICollection<cdConfirmationFormStatusDesc> cdConfirmationFormStatusDescs { get; set; }
        public virtual ICollection<cdConfirmationFormType> cdConfirmationFormTypes { get; set; }
        public virtual ICollection<prCurrAccOptInOptOutStatus> prCurrAccOptInOptOutStatuss { get; set; }
        public virtual ICollection<prCurrAccPersonalDataConfirmation> prCurrAccPersonalDataConfirmations { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
    }
}
