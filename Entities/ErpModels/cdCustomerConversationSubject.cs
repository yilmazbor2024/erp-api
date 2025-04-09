using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("cdCustomerConversationSubject")]
    public partial class cdCustomerConversationSubject
    {
        public cdCustomerConversationSubject()
        {
            cdCustomerConversationSubjectDescs = new HashSet<cdCustomerConversationSubjectDesc>();
            prCustomerConversations = new HashSet<prCustomerConversation>();
            prCustomerConversationSubjectRelatedResults = new HashSet<prCustomerConversationSubjectRelatedResult>();
        }

        [Key]
        [StringLength(20)]
        [Column(TypeName = "Char20")]
        public string CustomerConversationSubjectCode { get; set; }

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

        public virtual ICollection<cdCustomerConversationSubjectDesc> cdCustomerConversationSubjectDescs { get; set; }
        public virtual ICollection<prCustomerConversation> prCustomerConversations { get; set; }
        public virtual ICollection<prCustomerConversationSubjectRelatedResult> prCustomerConversationSubjectRelatedResults { get; set; }
    }
}
