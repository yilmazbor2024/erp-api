using System.Security.Claims;
using ErpMobile.Api.Models.Menu;
using ErpMobile.Api.Services.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpMobile.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet("my-menu")]
    public async Task<ActionResult<List<MenuItemDto>>> GetMyMenu()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var menuItems = await _menuService.GetUserMenuItemsAsync(userId);
        return Ok(menuItems);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<MenuItemDto>>> GetAll()
    {
        var menuItems = await _menuService.GetAllMenuItemsAsync();
        return Ok(menuItems);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemDto>> Get(string id)
    {
        var menuItem = await _menuService.GetMenuItemByIdAsync(id);
        if (menuItem == null)
        {
            return NotFound();
        }

        return Ok(menuItem);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<MenuItemDto>> Create([FromBody] MenuItemDto menuItem)
    {
        var createdItem = await _menuService.CreateMenuItemAsync(menuItem);
        return CreatedAtAction(nameof(Get), new { id = createdItem.Id }, createdItem);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<MenuItemDto>> Update(string id, [FromBody] MenuItemDto menuItem)
    {
        try
        {
            var updatedItem = await _menuService.UpdateMenuItemAsync(id, menuItem);
            return Ok(updatedItem);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _menuService.DeleteMenuItemAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
