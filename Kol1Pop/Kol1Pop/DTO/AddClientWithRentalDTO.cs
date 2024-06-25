using System.Runtime.InteropServices.JavaScript;

namespace Kol1Pop.DTO;

public class AddClientWithRentalDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string Address { get; set; }
    public CarRentDTO rent { get; set; }
}

public class CarRentDTO
{
    public int carId { get; set; }
    public DateTime dateFrom { get; set; }
    public DateTime dateTo { get; set; }
}