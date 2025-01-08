using WebShopApp.Infrastructure.Data.Domain;

namespace WebShopApp.Core.Contracts;

public interface ICategoryService
{
    List<Category> GetCategories();
    Category GetCategoryById(int id);
    List<Product> GetProductsByCategoryId(int id);
}