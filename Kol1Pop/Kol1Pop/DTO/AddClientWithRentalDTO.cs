using System.Runtime.InteropServices.JavaScript;

namespace Kol1Pop.DTO;

public class AddClientWithRentalDTO
{
    public ClientDTO client { get; set; } 
    public int carId { get; set; }
    public DateTime dateFrom { get; set; }
    public DateTime dateTo { get; set; }
}

public class ClientDTO
{
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}