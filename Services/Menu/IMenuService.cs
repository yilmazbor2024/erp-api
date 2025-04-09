using ErpMobile.Api.Models.Menu;

namespace ErpMobile.Api.Services.Menu;

public interface IMenuService
{
    Task<List<MenuItemDto>> GetAllMenuItemsAsync();
    Task<List<MenuItemDto>> GetUserMenuItemsAsync(string userId);
    Task<MenuItemDto?> GetMenuItemByIdAsync(string id);
    Task<MenuItemDto> CreateMenuItemAsync(MenuItemDto menuItem);
    Task<MenuItemDto> UpdateMenuItemAsync(string id, MenuItemDto menuItem);
    Task<bool> DeleteMenuItemAsync(string id);
}
