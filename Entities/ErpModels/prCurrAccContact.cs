using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccContact")]
    public partial class prCurrAccContact
    {
        public prCurrAccContact()
        {
            prCurrAccCommunications = new HashSet<prCurrAccCommunication>();
            prCurrAccDefaults = new HashSet<prCurrAccDefault>();
            prCurrAccPersonalInfos = new HashSet<prCurrAccPersonalInfo>();
            prCurrAccPostalAddresss = new HashSet<prCurrAccPostalAddress>();
            prCurrAccReconciliationContacts = new HashSet<prCurrAccReconciliationContact>();
            prCustomerConversations = new HashSet<prCustomerConversation>();
            prCustomerOnlinePaymentContacts = new HashSet<prCustomerOnlinePaymentContact>();
            prCustomerPresentCards = new HashSet<prCustomerPresentCard>();
            prSubCurrAccDefaults = new HashSet<prSubCurrAccDefault>();
            trBankLines = new HashSet<trBankLine>();
            trCashLines = new HashSet<trCashLine>();
            trChequeHeaders = new HashSet<trChequeHeader>();
            trContracts = new HashSet<trContract>();
            trCreditCardPaymentHeaders = new HashSet<trCreditCardPaymentHeader>();
            trCurrAccBooks = new HashSet<trCurrAccBook>();
            trDebitHeaders = new HashSet<trDebitHeader>();
            trDispOrderHeaders = new HashSet<trDispOrderHeader>();
            trExpenseSlipHeaders = new HashSet<trExpenseSlipHeader>();
            trGiftCardPaymentHeaders = new HashSet<trGiftCardPaymentHeader>();
            trInnerOrderHeaders = new HashSet<trInnerOrderHeader>();
            trInvoiceHeaders = new HashSet<trInvoiceHeader>();
            trItemTestHeaders = new HashSet<trItemTestHeader>();
            trOrderAsnHeaders = new HashSet<trOrderAsnHeader>();
            trOrderHeaders = new HashSet<trOrderHeader>();
            trOtherPaymentHeaders = new HashSet<trOtherPaymentHeader>();
            trPaymentHeaders = new HashSet<trPaymentHeader>();
            trPickingHeaders = new HashSet<trPickingHeader>();
            trProposalHeaders = new HashSet<trProposalHeader>();
            trReserveHeaders = new HashSet<trReserveHeader>();
            trShipmentHeaders = new HashSet<trShipmentHeader>();
            trSMSPoolLines = new HashSet<trSMSPoolLine>();
            trSupportRequestHeaders = new HashSet<trSupportRequestHeader>();
            trSurveyAnswerHeaders = new HashSet<trSurveyAnswerHeader>();
        }

        [Key]
        [Required]
        public Guid ContactID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ContactTypeCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string TitleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string JobTitleCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstName { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string LastName { get; set; }

        public string FirstLastName { get; set; }

        [Required]
        public bool IsAuthorized { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentityNum { get; set; }

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
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }
        public virtual cdJobTitle cdJobTitle { get; set; }
        public virtual cdContactType cdContactType { get; set; }
        public virtual cdTitle cdTitle { get; set; }

        public virtual ICollection<prCurrAccCommunication> prCurrAccCommunications { get; set; }
        public virtual ICollection<prCurrAccDefault> prCurrAccDefaults { get; set; }
        public virtual ICollection<prCurrAccPersonalInfo> prCurrAccPersonalInfos { get; set; }
        public virtual ICollection<prCurrAccPostalAddress> prCurrAccPostalAddresss { get; set; }
        public virtual ICollection<prCurrAccReconciliationContact> prCurrAccReconciliationContacts { get; set; }
        public virtual ICollection<prCustomerConversation> prCustomerConversations { get; set; }
        public virtual ICollection<prCustomerOnlinePaymentContact> prCustomerOnlinePaymentContacts { get; set; }
        public virtual ICollection<prCustomerPresentCard> prCustomerPresentCards { get; set; }
        public virtual ICollection<prSubCurrAccDefault> prSubCurrAccDefaults { get; set; }
        public virtual ICollection<trBankLine> trBankLines { get; set; }
        public virtual ICollection<trCashLine> trCashLines { get; set; }
        public virtual ICollection<trChequeHeader> trChequeHeaders { get; set; }
        public virtual ICollection<trContract> trContracts { get; set; }
        public virtual ICollection<trCreditCardPaymentHeader> trCreditCardPaymentHeaders { get; set; }
        public virtual ICollection<trCurrAccBook> trCurrAccBooks { get; set; }
        public virtual ICollection<trDebitHeader> trDebitHeaders { get; set; }
        public virtual ICollection<trDispOrderHeader> trDispOrderHeaders { get; set; }
        public virtual ICollection<trExpenseSlipHeader> trExpenseSlipHeaders { get; set; }
        public virtual ICollection<trGiftCardPaymentHeader> trGiftCardPaymentHeaders { get; set; }
        public virtual ICollection<trInnerOrderHeader> trInnerOrderHeaders { get; set; }
        public virtual ICollection<trInvoiceHeader> trInvoiceHeaders { get; set; }
        public virtual ICollection<trItemTestHeader> trItemTestHeaders { get; set; }
        public virtual ICollection<trOrderAsnHeader> trOrderAsnHeaders { get; set; }
        public virtual ICollection<trOrderHeader> trOrderHeaders { get; set; }
        public virtual ICollection<trOtherPaymentHeader> trOtherPaymentHeaders { get; set; }
        public virtual ICollection<trPaymentHeader> trPaymentHeaders { get; set; }
        public virtual ICollection<trPickingHeader> trPickingHeaders { get; set; }
        public virtual ICollection<trProposalHeader> trProposalHeaders { get; set; }
        public virtual ICollection<trReserveHeader> trReserveHeaders { get; set; }
        public virtual ICollection<trShipmentHeader> trShipmentHeaders { get; set; }
        public virtual ICollection<trSMSPoolLine> trSMSPoolLines { get; set; }
        public virtual ICollection<trSupportRequestHeader> trSupportRequestHeaders { get; set; }
        public virtual ICollection<trSurveyAnswerHeader> trSurveyAnswerHeaders { get; set; }
    }
}
