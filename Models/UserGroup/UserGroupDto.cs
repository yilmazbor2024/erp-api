namespace ErpMobile.Api.Models.UserGroup;

public class UserGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public List<ModulePermissionDto> Permissions { get; set; } = new();
}

public class ModulePermissionDto
{
    public string ModuleName { get; set; } = null!;
    public bool CanCreate { get; set; }
    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
}

public class CreateUserGroupRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<ModulePermissionDto> Permissions { get; set; } = new();
}

public class UpdateUserGroupRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<ModulePermissionDto> Permissions { get; set; } = new();
}
