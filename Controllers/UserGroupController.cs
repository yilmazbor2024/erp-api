using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.UserGroup;

namespace ErpMobile.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserGroupController : ControllerBase
{
    private readonly NanoServiceDbContext _context;
    private readonly ILogger<UserGroupController> _logger;

    public UserGroupController(
        NanoServiceDbContext context,
        ILogger<UserGroupController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserGroupDto>>> GetUserGroups()
    {
        var userGroups = await _context.UserGroups
            .Include(ug => ug.ModulePermissions)
            .Select(ug => new UserGroupDto
            {
                Id = ug.Id,
                Name = ug.Name,
                Description = ug.Description,
                IsActive = ug.IsActive,
                Permissions = ug.ModulePermissions.Select(mp => new ModulePermissionDto
                {
                    ModuleName = mp.ModuleName,
                    CanCreate = mp.CanCreate,
                    CanRead = mp.CanRead,
                    CanUpdate = mp.CanUpdate,
                    CanDelete = mp.CanDelete
                }).ToList()
            })
            .ToListAsync();

        return Ok(userGroups);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserGroupDto>> GetUserGroup(Guid id)
    {
        var userGroup = await _context.UserGroups
            .Include(ug => ug.ModulePermissions)
            .FirstOrDefaultAsync(ug => ug.Id == id);

        if (userGroup == null)
        {
            return NotFound();
        }

        return Ok(new UserGroupDto
        {
            Id = userGroup.Id,
            Name = userGroup.Name,
            Description = userGroup.Description,
            IsActive = userGroup.IsActive,
            Permissions = userGroup.ModulePermissions.Select(mp => new ModulePermissionDto
            {
                ModuleName = mp.ModuleName,
                CanCreate = mp.CanCreate,
                CanRead = mp.CanRead,
                CanUpdate = mp.CanUpdate,
                CanDelete = mp.CanDelete
            }).ToList()
        });
    }

    [HttpPost]
    public async Task<ActionResult<UserGroupDto>> CreateUserGroup(CreateUserGroupRequest request)
    {
        var userGroup = new UserGroup
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = User.Identity?.Name ?? "System",
            ModulePermissions = request.Permissions.Select(p => new ModulePermission
            {
                Id = Guid.NewGuid(),
                ModuleName = p.ModuleName,
                CanCreate = p.CanCreate,
                CanRead = p.CanRead,
                CanUpdate = p.CanUpdate,
                CanDelete = p.CanDelete,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = User.Identity?.Name ?? "System"
            }).ToList()
        };

        _context.UserGroups.Add(userGroup);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserGroup), new { id = userGroup.Id }, new UserGroupDto
        {
            Id = userGroup.Id,
            Name = userGroup.Name,
            Description = userGroup.Description,
            IsActive = userGroup.IsActive,
            Permissions = userGroup.ModulePermissions.Select(mp => new ModulePermissionDto
            {
                ModuleName = mp.ModuleName,
                CanCreate = mp.CanCreate,
                CanRead = mp.CanRead,
                CanUpdate = mp.CanUpdate,
                CanDelete = mp.CanDelete
            }).ToList()
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserGroup(Guid id, UpdateUserGroupRequest request)
    {
        var userGroup = await _context.UserGroups
            .Include(ug => ug.ModulePermissions)
            .FirstOrDefaultAsync(ug => ug.Id == id);

        if (userGroup == null)
        {
            return NotFound();
        }

        userGroup.Name = request.Name;
        userGroup.Description = request.Description;
        userGroup.ModifiedAt = DateTime.UtcNow;
        userGroup.ModifiedBy = User.Identity?.Name;

        // Remove existing permissions
        _context.ModulePermissions.RemoveRange(userGroup.ModulePermissions);

        // Add new permissions
        userGroup.ModulePermissions = request.Permissions.Select(p => new ModulePermission
        {
            Id = Guid.NewGuid(),
            ModuleName = p.ModuleName,
            CanCreate = p.CanCreate,
            CanRead = p.CanRead,
            CanUpdate = p.CanUpdate,
            CanDelete = p.CanDelete,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = User.Identity?.Name ?? "System"
        }).ToList();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserGroup(Guid id)
    {
        var userGroup = await _context.UserGroups.FindAsync(id);

        if (userGroup == null)
        {
            return NotFound();
        }

        _context.UserGroups.Remove(userGroup);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
