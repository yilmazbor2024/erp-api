using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdSMSJobType")]
    public partial class cdSMSJobType
    {
        public cdSMSJobType()
        {
            cdSMSJobTypeDescs = new HashSet<cdSMSJobTypeDesc>();
            dfSMSForCustomerRelationships = new HashSet<dfSMSForCustomerRelationship>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string SMSJobTypeCode { get; set; }

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

        public virtual ICollection<cdSMSJobTypeDesc> cdSMSJobTypeDescs { get; set; }
        public virtual ICollection<dfSMSForCustomerRelationship> dfSMSForCustomerRelationships { get; set; }
    }
}
