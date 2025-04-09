using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerConversationResult")]
    public partial class cdCustomerConversationResult
    {
        public cdCustomerConversationResult()
        {
            cdCustomerConversationResultDescs = new HashSet<cdCustomerConversationResultDesc>();
            prCustomerConversations = new HashSet<prCustomerConversation>();
            prCustomerConversationSubjectRelatedResults = new HashSet<prCustomerConversationSubjectRelatedResult>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerConversationResultCode { get; set; }

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

        public virtual ICollection<cdCustomerConversationResultDesc> cdCustomerConversationResultDescs { get; set; }
        public virtual ICollection<prCustomerConversation> prCustomerConversations { get; set; }
        public virtual ICollection<prCustomerConversationSubjectRelatedResult> prCustomerConversationSubjectRelatedResults { get; set; }
    }
}
