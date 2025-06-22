namespace eShop.Web.Models.ViewModels;

public class PagedResult<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
