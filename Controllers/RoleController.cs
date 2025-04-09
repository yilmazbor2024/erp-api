using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Models.Role;
using ErpMobile.Api.Entities;

namespace ErpMobile.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RoleController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleManager.Roles
            .Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name ?? "",
                Description = r.Description ?? "",
                IsActive = r.IsActive
            })
            .ToListAsync();

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id is required");
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        return Ok(new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? "",
            Description = role.Description ?? "",
            IsActive = role.IsActive
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request is null");
        }

        if (string.IsNullOrEmpty(request.Name))
        {
            return BadRequest("Role name is required");
        }

        var role = new Role
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = User.Identity?.Name ?? "System"
        };

        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? "",
            Description = role.Description ?? "",
            IsActive = role.IsActive
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleRequest request)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id is required");
        }

        if (request == null)
        {
            return BadRequest("Request is null");
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();
        }

        if (request.Description != null)
        {
            role.Description = request.Description;
        }

        if (request.IsActive.HasValue)
        {
            role.IsActive = request.IsActive.Value;
        }

        role.ModifiedAt = DateTime.UtcNow;
        role.ModifiedBy = User.Identity?.Name ?? "System";

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? "",
            Description = role.Description ?? "",
            IsActive = role.IsActive
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id is required");
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name ?? "");
        if (usersInRole.Any())
        {
            return BadRequest("Cannot delete role with users");
        }

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetUsersInRole(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id is required");
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        var users = await _userManager.GetUsersInRoleAsync(role.Name ?? "");
        var userDtos = users.Select(u => new UserInRoleDto
        {
            Id = u.Id,
            UserName = u.UserName ?? u.Email,
            Email = u.Email ?? "",
            UserCode = u.UserCode,
            FirstName = u.FirstName,
            LastName = u.LastName
        }).ToList();

        return Ok(userDtos);
    }
}
