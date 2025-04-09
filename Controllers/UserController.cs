using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ErpMobile.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly NanoServiceDbContext _context;

    public UserController(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        NanoServiceDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    [HttpGet("current")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return await GetUserDto(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userManager.Users
            .Include(u => u.UserGroup)
            .Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName!,
                Email = u.Email!,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsActive = u.IsActive,
                UserGroupId = u.UserGroup != null ? u.UserGroup.Id.ToString() : null,
                UserGroupName = u.UserGroup != null ? u.UserGroup.Name : null
            })
            .ToListAsync();

        foreach (var user in users)
        {
            var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
            user.Roles = userRoles.ToList();
        }

        return users;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserRequest request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = true,
            UserCode = Guid.NewGuid().ToString("N")[..8].ToUpper(),
            CreatedAt = DateTime.UtcNow,
            CreatedBy = User.Identity?.Name ?? "system"
        };

        if (!string.IsNullOrEmpty(request.UserGroupId))
        {
            var userGroup = await _context.UserGroups.FindAsync(Guid.Parse(request.UserGroupId));
            if (userGroup != null)
            {
                user.UserGroup = userGroup;
            }
        }

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        if (request.Roles.Any())
        {
            result = await _userManager.AddToRolesAsync(user, request.Roles);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
        }

        return await GetUserDto(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(string id, UpdateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.IsActive = request.IsActive;

        if (!string.IsNullOrEmpty(request.UserGroupId))
        {
            var userGroup = await _context.UserGroups.FindAsync(Guid.Parse(request.UserGroupId));
            if (userGroup != null)
            {
                user.UserGroup = userGroup;
            }
        }
        else
        {
            user.UserGroup = null;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        if (request.Roles.Any())
        {
            result = await _userManager.AddToRolesAsync(user, request.Roles);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
        }

        return await GetUserDto(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    private async Task<UserDto> GetUserDto(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsActive = user.IsActive,
            Roles = roles.ToList(),
            UserGroupId = user.UserGroup?.Id.ToString(),
            UserGroupName = user.UserGroup?.Name
        };
    }
}
