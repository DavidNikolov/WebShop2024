using WebShopApp.Core.Contracts;
using WebShopApp.Infrastructure.Data;
using WebShopApp.Infrastructure.Data.Domain;

namespace WebShopApp.Core.Services;

public class BrandService:IBrandService
{
    private readonly ApplicationDbContext _context;

    public BrandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Brand> GetBrands()
    {
        return _context.Brands.ToList();
    }

    public Brand GetBrandById(int id)
    {
        return _context.Brands.Find(id);
    }

    public List<Product> GetProductsByBrandId(int id)
    {
        return _context.Products.Where(p => p.BrandId == id).ToList();
    }
}