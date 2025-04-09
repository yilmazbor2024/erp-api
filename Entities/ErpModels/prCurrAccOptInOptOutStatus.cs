using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccOptInOptOutStatus")]
    public partial class prCurrAccOptInOptOutStatus
    {
        public prCurrAccOptInOptOutStatus()
        {
        }

        [Key]
        [Required]
        public Guid CommunicationID { get; set; }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CompanyBrandCode { get; set; }

        [Required]
        public bool CanSendAdvert { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string FormNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string ConfirmationFormStatusCode { get; set; }

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
        public virtual cdConfirmationFormType cdConfirmationFormType { get; set; }
        public virtual prCurrAccCommunication prCurrAccCommunication { get; set; }
        public virtual cdCompanyBrand cdCompanyBrand { get; set; }
        public virtual cdConfirmationFormStatus cdConfirmationFormStatus { get; set; }

    }
}
