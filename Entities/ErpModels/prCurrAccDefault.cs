using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("prCurrAccDefault")]
    public partial class prCurrAccDefault
    {
        public prCurrAccDefault()
        {
        }

        [Key]
        [Required]
        public byte CurrAccTypeCode { get; set; }

        [Key]
        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        public Guid? ContactID { get; set; }

        public Guid? PostalAddressID { get; set; }

        public Guid? ShippingAddressID { get; set; }

        public Guid? BillingAddressID { get; set; }

        public Guid? CommunicationID { get; set; }

        public Guid? SubCurrAccID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CardNumber { get; set; }

        public Guid? EArchieveEMailCommunicationID { get; set; }

        public Guid? EArchieveMobileCommunicationID { get; set; }

        public Guid? OfficePhoneID { get; set; }

        public Guid? HomePhoneID { get; set; }

        public Guid? BusinessMobileID { get; set; }

        public Guid? PersonalMobileID { get; set; }

        public Guid? GuidedSalesNotificationEmailID { get; set; }

        public Guid? GuidedSalesNotificationPhoneID { get; set; }

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
        public virtual prCurrAccPostalAddress prCurrAccPostalAddress { get; set; }
        public virtual prCurrAccCommunication prCurrAccCommunication { get; set; }
        public virtual prCurrAccContact prCurrAccContact { get; set; }
        public virtual prCustomerPresentCard prCustomerPresentCard { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual prSubCurrAcc prSubCurrAcc { get; set; }

    }
}
