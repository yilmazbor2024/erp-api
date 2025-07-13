using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Auth
{
    [Table("UserGroupMembers", Schema = "dbo")]
    public class UserGroupMember
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UserGroupId { get; set; }
        
        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public string ModifiedBy { get; set; }
    }
}
