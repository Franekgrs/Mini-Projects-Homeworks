using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Service;

namespace WebApplication1.Controller;
[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpPost]
    public async Task<IActionResult> AddClientToTrip(ClientToAddDto clientToAddDto)
    {
        try
        {
            await _tripService.AssignClientToTrip(clientToAddDto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTripsAsync()
    {
        return Ok(await _tripService.GetTripsAsync());
    }
}