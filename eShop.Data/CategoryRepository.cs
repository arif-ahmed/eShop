using eShop.Models.Catalog;

namespace eShop.Data;

public class CategoryRepository : BaseRepository<Category>
{
    public CategoryRepository(EShopDbContext context) : base(context)
    {

    }
}
