using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdInactivationReason")]
    public partial class cdInactivationReason
    {
        public cdInactivationReason()
        {
            cdInactivationReasonDescs = new HashSet<cdInactivationReasonDesc>();
            dfCompanyDefaults = new HashSet<dfCompanyDefault>();
            prCurrAccPersonalDataConfirmations = new HashSet<prCurrAccPersonalDataConfirmation>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InactivationReasonCode { get; set; }

        [Required]
        public bool CanNotBeReActivated { get; set; }

        [Required]
        public bool CanCreateNewPresentCard { get; set; }

        [Required]
        public bool UseStore { get; set; }

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

        public virtual ICollection<cdInactivationReasonDesc> cdInactivationReasonDescs { get; set; }
        public virtual ICollection<dfCompanyDefault> dfCompanyDefaults { get; set; }
        public virtual ICollection<prCurrAccPersonalDataConfirmation> prCurrAccPersonalDataConfirmations { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
    }
}
