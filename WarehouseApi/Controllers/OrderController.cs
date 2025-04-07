using Microsoft.AspNetCore.Mvc;
using zadanie7.Models;
using zadanie7.Services;

namespace zadanie7.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}/{amount}/{createdAt}")]
    public async Task<ActionResult<Order>> GetByProductIdAndAmount(int id, int amount, DateTime createdAt)
    {
        var order = await _orderService.GetByProductIdAndAmount(id, amount, createdAt);
        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

}