using Microsoft.AspNetCore.Mvc;
using zadanie7.Models;
using zadanie7.Services;

namespace zadanie7.Controllers;

public class AddProductToWarehouseRequest
{
    public int IdProduct { get; set; }
    public int IdWarehouse { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class ProductWarehouseController : ControllerBase
{
    private readonly IProductWarehouseService _productWarehouseService;

    public ProductWarehouseController(IProductWarehouseService productWarehouseService)
    {
        _productWarehouseService = productWarehouseService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddProductToWarehouse(AddProductToWarehouseRequest request)
    {
        try
        {
            // Tutaj możemy wywołać metodę z serwisu _productWarehouseService, przekazując request jako argument
            var productWarehouse = new ProductWarehouse
            {
                IdProduct = request.IdProduct,
                IdWarehouse = request.IdWarehouse,
                Amount = request.Amount,
                CreatedAt = request.CreatedAt
            };
            var id = await _productWarehouseService.AddProductToWarehouse(productWarehouse);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}