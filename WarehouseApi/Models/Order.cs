using System.ComponentModel.DataAnnotations;

namespace zadanie7.Models;

public class Order
{
    [Required]
    public int IdOrder { get; set; }
    [Required]
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FullfilledAt { get; set; }
}