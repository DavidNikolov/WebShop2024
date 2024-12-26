using System.ComponentModel.DataAnnotations;

namespace WebShopApp.Infrastructure.Domain;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string CategoryName { get; set; } = null!;

    public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
}