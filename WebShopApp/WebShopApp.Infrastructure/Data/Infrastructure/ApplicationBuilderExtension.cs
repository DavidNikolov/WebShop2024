using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebShopApp.Infrastructure.Data.Domain;

namespace WebShopApp.Infrastructure.Data.Infrastructure;

public static class ApplicationBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var services = serviceScope.ServiceProvider;

        await RoleSeeder(services);
        await SeedAdministrator(services);
        
        var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        SeedCategories(dataCategory);
        
        var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        SeedBrands(dataBrand);

        return app;
    }

    private static async Task RoleSeeder(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roleNames = { "Admin", "Client" };
        IdentityResult roleResult;
        foreach (var role in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdministrator(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        if (await userManager.FindByNameAsync("admin") == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.FirstName = "admin";
            user.LastName = "admin";
            user.UserName = "admin";
            user.Email = "admin@admin.com";
            user.Address = "admin address";
            user.PhoneNumber = "0888888888";

            var result = await userManager.CreateAsync(user, "Admin123");

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }

    private static void SeedCategories(ApplicationDbContext context)
    {
        if (context.Categories.Any()) return;
        
        context.Categories.AddRange(new[] 
        {
            new Category{CategoryName = "Laptop"},  
            new Category{CategoryName = "Computer"},  
            new Category{CategoryName = "Monitor"},  
            new Category{CategoryName = "Accessory"},  
            new Category{CategoryName = "TV"},  
            new Category{CategoryName = "Mobile phone"},  
            new Category{CategoryName = "Smart watch"},  
        });
        context.SaveChanges();
    }

    private static void SeedBrands(ApplicationDbContext context)
    {
        if (context.Categories.Any()) return;
        
        context.Brands.AddRange(new[]
        {
            new Brand {BrandName = "Acer"},
            new Brand {BrandName = "Asus"},
            new Brand {BrandName = "Apple"},
            new Brand {BrandName = "Dell"},
            new Brand {BrandName = "HP"},
            new Brand {BrandName = "Huawei"},
            new Brand {BrandName = "Lenovo"},
            new Brand {BrandName = "Samsung"},
        });
        context.SaveChanges();
    }
    
}