using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdScheduleReSendSMSForCustomerRelationship")]
    public partial class cdScheduleReSendSMSForCustomerRelationship
    {
        public cdScheduleReSendSMSForCustomerRelationship()
        {
            dfReSendSMSForCustomerRelationships = new HashSet<dfReSendSMSForCustomerRelationship>();
        }

        [Key]
        [Required]
        public int ScheduleID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ScheduleName { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

        public string jsonData { get; set; }

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

        public virtual ICollection<dfReSendSMSForCustomerRelationship> dfReSendSMSForCustomerRelationships { get; set; }
    }
}
