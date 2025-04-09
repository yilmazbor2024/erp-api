using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCustomerConversation")]
    public partial class prCustomerConversation
    {
        public prCustomerConversation()
        {
        }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FormNumber { get; set; }

        [Required]
        public object CompanyCode { get; set; }

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [Required]
        public byte TalkedWith { get; set; }

        public Guid? ContactID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string FirstLastName { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerConversationSubjectCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerConversationResultCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string Description { get; set; }

        [Required]
        public DateTime NextConversationDate { get; set; }

        [Required]
        public byte CallType { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerConversationSubtitleCode { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerConversationSubjectDetailCode { get; set; }

        [StringLength(10)]
        [Column(TypeName = "Char10")]
        public string ApplicationCode { get; set; }

        [Required]
        public bool IsPaymentPromised { get; set; }

        [Required]
        public DateTime PromisedPaymentDate { get; set; }

        [Required]
        public decimal PromisedPaymentAmount { get; set; }

        public Guid? ApplicationID { get; set; }

        [Required]
        public byte RelatedStoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string RelatedStoreCode { get; set; }

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
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdCustomerConversationSubjectDetail cdCustomerConversationSubjectDetail { get; set; }
        public virtual cdCustomerConversationSubtitle cdCustomerConversationSubtitle { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdCustomerConversationSubject cdCustomerConversationSubject { get; set; }
        public virtual bsApplication bsApplication { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdCustomerConversationResult cdCustomerConversationResult { get; set; }

    }
}
