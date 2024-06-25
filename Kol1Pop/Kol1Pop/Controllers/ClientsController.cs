using Kol1Pop.DTO;
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
    public async Task<IActionResult> AddClientWithRental(AddClientWithRentalDTO clientWithRentalDto)
    {
        if (!await _dbService.DoesCarExist(clientWithRentalDto.rent.carId))
        {
            return NotFound("Car with given id does not exist!");
        }
        await _dbService.AddClientWithCar(clientWithRentalDto);
        return Created();
    }
}