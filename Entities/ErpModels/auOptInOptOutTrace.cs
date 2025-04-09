using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("auOptInOptOutTrace")]
    public partial class auOptInOptOutTrace
    {
        public auOptInOptOutTrace()
        {
        }

        [Key]
        [Required]
        public Guid OptInOptOutTraceID { get; set; }

        [Required]
        public Guid CommunicationID { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Required]
        public byte StatusType { get; set; }

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

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FormNumber { get; set; }

        public string OptDescription { get; set; }

        [Required]
        public object CompanyCode { get; set; }

     

        [Required]
        public object OfficeCode { get; set; }

        [Required]
        public byte StoreTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string StoreCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string IdentificationMark { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MessageResponseID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PermissionMarketingServiceCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PermissionMarketingServiceUID { get; set; }

        [Required]
        public bool SMS { get; set; }

        [Required]
        public bool Call { get; set; }

        [Required]
        public bool Email { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConsentSource { get; set; }

        [Required]
        public DateTime ConsentTime { get; set; }

        [Required]
        public byte RecipientType { get; set; }

        [Required]
        public bool IsSentToBusinessPartner { get; set; }

        [Required]
        public DateTime SentDate { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MMSBusinessPartnerCode { get; set; }

        public string TransactionID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

        public Guid? CurrAccPersonalDataConfirmationID { get; set; }

        // Navigation Properties
        public virtual cdPermissionMarketingService cdPermissionMarketingService { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdCommunicationType cdCommunicationType { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
