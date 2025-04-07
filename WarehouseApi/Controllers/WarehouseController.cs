using Microsoft.AspNetCore.Mvc;
using zadanie7.Models;
using zadanie7.Services;

namespace zadanie7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Warehouse>> GetWarehouseById(int id)
    {
        var warehouseCount = await _warehouseService.ExistsWarehouseById(id);
        if (!warehouseCount)
        {
            return NotFound();
        }
        
        var warehouse = await _warehouseService.GetWarehouseById(id);
        if (warehouse == null)
        {
            return NotFound();
        }
        return warehouse;
    }
    
    
    
}