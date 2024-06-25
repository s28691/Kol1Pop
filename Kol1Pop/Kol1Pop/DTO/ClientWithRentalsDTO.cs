namespace Kol1Pop.DTO;

public class ClientWithRentalsDTO
{
    public int id { get; set; }
    public string firstname { get; set; }
    public string lastName { get; set; }
    public string address { get; set; }
    public List<RentalDTO> rentals { get; set; }
}

public class RentalDTO
{
    public string vin { get; set; }
    public string color { get; set; }
    public string model { get; set; }
    public DateTime dateFrom { get; set; }
    public DateTime dateTo { get; set; }
    public int totalPrice { get; set; }
}