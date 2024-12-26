using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopApp.Infrastructure.Domain;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string ProductName { get; set; } = null!;
    
    [Required]
    [ForeignKey(nameof(Brand))]
    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; } = null!;
    
    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
    
    public string Picture { get; set; } = null!;
    
    [Range(0, 5000)]
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Discount { get; set; }
    
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
}