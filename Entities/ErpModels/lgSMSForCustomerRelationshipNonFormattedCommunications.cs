using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("lgSMSForCustomerRelationshipNonFormattedCommunications")]
    public partial class lgSMSForCustomerRelationshipNonFormattedCommunications
    {
        public lgSMSForCustomerRelationshipNonFormattedCommunications()
        {
        }

        [Key]
        [Required]
        public Guid SMSForCustomerRelationshipNonFormattedCommunicationsID { get; set; }

        [Required]
        public Guid SMSForCustomerRelationshipID { get; set; }

        [Required]
        public byte CurrAccTypeCode { get; set; }

        [StringLength(100)]
        [Column(TypeName = "Char100")]
        public string CurrAccCode { get; set; }

        [StringLength(50)]
        [Column(TypeName = "Char50")]
        public string CommunicationTypeCode { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CommAddress { get; set; }

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
        public virtual dfSMSForCustomerRelationship dfSMSForCustomerRelationship { get; set; }
        public virtual cdCurrAcc cdCurrAcc { get; set; }
        public virtual cdCommunicationType cdCommunicationType { get; set; }

    }
}
