using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.Menu;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErpMobile.Api.Services.Menu;

public class MenuService : IMenuService
{
    private readonly NanoServiceDbContext _context;

    public MenuService(NanoServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<MenuItemDto>> GetAllMenuItemsAsync()
    {
        var menuItems = await _context.MenuItems
            .OrderBy(x => x.Order)
            .ToListAsync();

        return menuItems.Select(MapToDto).ToList();
    }

    public async Task<List<MenuItemDto>> GetUserMenuItemsAsync(string userId)
    {
        // Get user roles
        var userRoles = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(_context.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => r.Name!)
            .ToListAsync();

        // Get menu items based on user roles
        var menuItems = await _context.MenuItems
            .Where(m => m.IsVisible && m.Roles.Any(r => userRoles.Contains(r)))
            .OrderBy(m => m.Order)
            .ToListAsync();

        return menuItems.Select(MapToDto).ToList();
    }

    public async Task<MenuItemDto?> GetMenuItemByIdAsync(string id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        return menuItem != null ? MapToDto(menuItem) : null;
    }

    public async Task<MenuItemDto> CreateMenuItemAsync(MenuItemDto menuItemDto)
    {
        var menuItem = MapToEntity(menuItemDto);
        await _context.MenuItems.AddAsync(menuItem);
        await _context.SaveChangesAsync();
        return MapToDto(menuItem);
    }

    public async Task<MenuItemDto> UpdateMenuItemAsync(string id, MenuItemDto menuItemDto)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null)
        {
            throw new KeyNotFoundException($"MenuItem with id {id} not found");
        }

        // Update properties
        menuItem.Title = menuItemDto.Title;
        menuItem.Icon = menuItemDto.Icon;
        menuItem.Url = menuItemDto.Url;
        menuItem.ParentId = menuItemDto.ParentId;
        menuItem.Order = menuItemDto.Order;
        menuItem.IsVisible = menuItemDto.IsVisible;
        menuItem.Roles = menuItemDto.Roles;

        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
        return MapToDto(menuItem);
    }

    public async Task<bool> DeleteMenuItemAsync(string id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null)
        {
            return false;
        }

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
        return true;
    }

    private static MenuItemDto MapToDto(MenuItem entity)
    {
        return new MenuItemDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Icon = entity.Icon,
            Url = entity.Url,
            ParentId = entity.ParentId,
            Order = entity.Order,
            IsVisible = entity.IsVisible,
            Roles = entity.Roles
        };
    }

    private static MenuItem MapToEntity(MenuItemDto dto)
    {
        return new MenuItem
        {
            Id = dto.Id,
            Title = dto.Title,
            Icon = dto.Icon,
            Url = dto.Url,
            ParentId = dto.ParentId,
            Order = dto.Order,
            IsVisible = dto.IsVisible,
            Roles = dto.Roles
        };
    }
}
