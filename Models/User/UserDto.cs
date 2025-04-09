using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.User;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool IsActive { get; set; }
    public List<string> Roles { get; set; } = new();
    public string? UserGroupId { get; set; }
    public string? UserGroupName { get; set; }
}

public class CreateUserRequest
{
    [Required]
    public string UserName { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
    
    public string? UserGroupId { get; set; }
    public List<string> Roles { get; set; } = new();
}

public class UpdateUserRequest
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    public string? UserGroupId { get; set; }
    public List<string> Roles { get; set; } = new();
    public bool IsActive { get; set; }
}
