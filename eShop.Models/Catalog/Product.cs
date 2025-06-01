namespace eShop.Models.Catalog;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public string? SKU { get; set; }
    public string? ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}
