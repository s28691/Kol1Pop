using Kol1Pop.DTO;

namespace Kol1Pop.Services;

public interface IDbService
{
    Task<bool> DoesClientExist(int id);
    Task<ClientWithRentalsDTO> GetClientWithRentals(int id);
}