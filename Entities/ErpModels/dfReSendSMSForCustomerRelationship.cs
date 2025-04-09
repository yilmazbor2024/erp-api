using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("dfReSendSMSForCustomerRelationship")]
    public partial class dfReSendSMSForCustomerRelationship
    {
        public dfReSendSMSForCustomerRelationship()
        {
        }

        [Key]
        [Required]
        public Guid ReSendSMSForCustomerRelationshipID { get; set; }

        [Required]
        public int ScheduleID { get; set; }

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
        public virtual cdScheduleReSendSMSForCustomerRelationship cdScheduleReSendSMSForCustomerRelationship { get; set; }

    }
}
