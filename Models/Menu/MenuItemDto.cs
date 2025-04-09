namespace ErpMobile.Api.Models.Menu;

public class MenuItemDto
{
    public string Id { get; set; } = null!;
    public string? ParentId { get; set; }
    public string Title { get; set; } = null!;
    public string? Icon { get; set; }
    public string? Url { get; set; }
    public int Order { get; set; }
    public bool IsVisible { get; set; } = true;
    public List<string> Roles { get; set; } = new();
}
