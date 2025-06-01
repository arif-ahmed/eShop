using System.Collections.ObjectModel;

namespace eShop.Models.Catalog;
public class Category : EntityBase
{
    public string? Name { get; set; }
    public ICollection<Product> Products { get; set; } = new Collection<Product>();
}
