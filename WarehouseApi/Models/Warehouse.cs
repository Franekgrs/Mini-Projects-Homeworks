using System.ComponentModel.DataAnnotations;

namespace zadanie7.Models;

public class Warehouse
{
    [Required]
    public int IdWarehouse { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Address { get; set; }
}