using System.ComponentModel.DataAnnotations;

namespace WebShopApp.Infrastructure.Data.Domain;

public class Brand
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string BrandName { get; set; } = null!;

    public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

}