using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpMobile.Api.Models.Auth
{
    [Table("ModulePermissions", Schema = "dbo")]
    public class ModulePermission
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string ModuleName { get; set; }
        
        [Required]
        public int UserGroupId { get; set; }
        
        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }
        
        [Required]
        public bool CanView { get; set; }
        
        [Required]
        public bool CanCreate { get; set; }
        
        [Required]
        public bool CanEdit { get; set; }
        
        [Required]
        public bool CanDelete { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public string ModifiedBy { get; set; }
    }
}
