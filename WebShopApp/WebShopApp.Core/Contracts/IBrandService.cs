using WebShopApp.Infrastructure.Data.Domain;

namespace WebShopApp.Core.Contracts;

public interface IBrandService
{
    List<Brand> GetBrands();
    Brand GetBrandById(int id);
    List<Product> GetProductsByBrandId(int id);
}