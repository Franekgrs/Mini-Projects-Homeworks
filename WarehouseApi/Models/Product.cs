using System.ComponentModel.DataAnnotations;

namespace zadanie7.Models;

public class Product
{
    [Required]
    public int IdProduct { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    public decimal Price { get; set; }
    
}