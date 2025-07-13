using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Auth
{
    [Table("UserGroups", Schema = "dbo")]
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string GroupName { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public string ModifiedBy { get; set; }
        
        // Navigation properties
        public virtual ICollection<ModulePermission> ModulePermissions { get; set; }
        public virtual ICollection<UserGroupMember> UserGroupMembers { get; set; }
    }
}
