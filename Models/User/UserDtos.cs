using System.ComponentModel.DataAnnotations;

namespace ErpMobile.Api.Models.User;

public class UserListDto
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required List<string> Roles { get; set; } = new();
}

public class UserDetailDto
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required List<string> Roles { get; set; } = new();
}

public class CreateUserDto
{
    [Required]
    [StringLength(50)]
    public required string UserName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public required string Password { get; set; }

    public List<string> Roles { get; set; } = new();
}

public class UpdateUserDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }

    public List<string> Roles { get; set; } = new();
}
