using Microsoft.AspNetCore.Mvc;
using WebApplication1.Exceptions;
using WebApplication1.Service;

namespace WebApplication1.Controller;
[Route("api/[controller]")]
[ApiController]
public class ClientController: ControllerBase
{
    private readonly iClientService _clientService;

    public ClientController(iClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        try
        {
            var result = await _clientService.DeleteCkuebtAsync(id);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        
        return NoContent();
    }
}