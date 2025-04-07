using Microsoft.AspNetCore.Mvc;
using WarehouseApiPrzedKolosem.DTOs;
using WarehouseApiPrzedKolosem.Services;

namespace WarehouseApiPrzedKolosem.Controllers;
[Route("api/productWarehouse")]
[ApiController]
public class ProductWarehouseController : ControllerBase
{
    private readonly IProductWarehouseService _productWarehouseService;

    public ProductWarehouseController(IProductWarehouseService productWarehouseService)
    {
        _productWarehouseService = productWarehouseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToWarehouse(WarehouseDto warehouseDto)
    {
        return Ok(await _productWarehouseService.AddToProductWarehouse(warehouseDto));
    }
}