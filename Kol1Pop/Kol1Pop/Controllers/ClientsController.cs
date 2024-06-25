using Kol1Pop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kol1Pop.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IDbService _dbService;
    public ClientsController(IDbService iDbService)
    {
        _dbService = iDbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        if (! await _dbService.DoesClientExist(id))
        {
            return NotFound("Client with given id does not exist!");
        }
        var result = await _dbService.GetClientWithRentals(id);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddClientWithRental(Add)
}