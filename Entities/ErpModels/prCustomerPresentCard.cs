using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomerPresentCard")]
    public partial class prCustomerPresentCard
    {
        public prCustomerPresentCard()
        {
            prCurrAccDefaults = new HashSet<prCurrAccDefault>();
        }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CardNumber { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PresentCardTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerCRMGroupCode { get; set; }

        [Required]
        public bool IsCRMFormFilled { get; set; }

        [Required]
        public bool IsCRMActive { get; set; }

        [Required]
        public byte ActivationStatusCode { get; set; }

        [Required]
        public DateTime ActivationDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FormNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormStatusCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string InactivationReasonCode { get; set; }

        [Required]
        public bool IsCreditCard { get; set; }

        [Required]
        public DateTime LastValidDate { get; set; }

        [Required]
        public short CSV { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CreditCardTypeCode { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public short PosTerminalID { get; set; }

        public Guid? CurrAccPersonalDataConfirmationID { get; set; }

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
        public virtual bsPresentCardActivationStatus bsPresentCardActivationStatus { get; set; }
        public virtual cdPresentCardType cdPresentCardType { get; set; }
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdInactivationReason cdInactivationReason { get; set; }
        public virtual cdCustomerCRMGroup cdCustomerCRMGroup { get; set; }
        public virtual cdCreditCardType cdCreditCardType { get; set; }
        public virtual cdConfirmationFormStatus cdConfirmationFormStatus { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

        public virtual ICollection<prCurrAccDefault> prCurrAccDefaults { get; set; }
    }
}
