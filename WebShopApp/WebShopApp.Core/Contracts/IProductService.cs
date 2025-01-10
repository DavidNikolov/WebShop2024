using WebShopApp.Infrastructure.Data.Domain;

namespace WebShopApp.Core.Contracts;

public interface IProductService
{
    bool Create (string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);
    bool Update (int productId, string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);
    List<Product> GetProducts();
    Product GetProductById(int id);
    bool RemoveProductById(int id);
    List<Product> GetProducts (string searchStringCategoryName, string searchStringBrandName);
}