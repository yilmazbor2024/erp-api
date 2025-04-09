using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.Role;

public class CreateRoleRequest
{
    [Required]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
}
