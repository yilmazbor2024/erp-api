using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Entities.ErpModels
{
    [Table("rpEuromessageCampaignConversations")]
    public partial class rpEuromessageCampaignConversations
    {
        public rpEuromessageCampaignConversations()
        {
        }

        [Required]
        public Guid BulkMailServiceProviderAccountID { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string CampaignID { get; set; }

        [Key]
        [StringLength(200)]
        [Column(TypeName = "Char200")]
        public string ConversationID { get; set; }

        [Required]
        public bool Status { get; set; }

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

    }
}
