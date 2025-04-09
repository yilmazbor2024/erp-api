using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccPersonalDataConfirmation")]
    public partial class prCurrAccPersonalDataConfirmation
    {
        public prCurrAccPersonalDataConfirmation()
        {
        }

        [Key]
        [Required]
        public Guid CurrAccPersonalDataConfirmationID { get; set; }

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

        public Guid? ContactID { get; set; }

        [Required]
        public short POSTerminalID { get; set; }

        [Required]
        public DateTime ConfirmationDate { get; set; }

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
        public DateTime InactivationDate { get; set; }

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

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string MessageResponseID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string PermissionMarketingServiceCode { get; set; }

        [StringLength(200400)]
        [Column(TypeName = "Char200(400)")]
        public string PermissionMarketingServiceUID { get; set; }

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
        public virtual cdPermissionMarketingService cdPermissionMarketingService { get; set; }
        public virtual cdCompany cdCompany { get; set; }
        public virtual cdOffice cdOffice { get; set; }
        public virtual cdInactivationReason cdInactivationReason { get; set; }
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual cdConfirmationFormStatus cdConfirmationFormStatus { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }

    }
}
