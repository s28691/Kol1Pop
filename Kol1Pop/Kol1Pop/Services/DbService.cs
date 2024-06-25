using Kol1Pop.Controllers;
using Kol1Pop.DTO;
using Microsoft.Data.SqlClient;

namespace Kol1Pop.Services;

public class DbService : IDbService
{
    private readonly IConfiguration _configuration;
    public DbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesClientExist(int id)
    {
        var query = "SELECT 1 FROM clients WHERE id = @ID";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<ClientWithRentalsDTO> GetClientWithRentals(int id)
    {
        var query =
            @"SELECT clients.id AS ClientId, FirstName, LastName, Address, VIN, colors.name AS Color, models.Name AS model, DateFrom, DateTo, TotalPrice FROM clients 
    JOIN car_rentals ON clients.ID=car_rentals.ClientID 
    JOIN cars ON car_rentals.CarID=cars.ID 
    JOIN colors ON cars.ColorID=colors.ID 
    JOIN models ON cars.ModelID=models.ID WHERE clients.id = @ID;";
        
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
	    
        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        var clientIdOrdinal = reader.GetOrdinal("ClientId");
        var firstNameOrdinal = reader.GetOrdinal("FirstName");
        var lastnameOrdinal = reader.GetOrdinal("LastName");
        var addressOrdinal = reader.GetOrdinal("Address");
        var vinOrdinal = reader.GetOrdinal("VIN");
        var colorOrdinal = reader.GetOrdinal("Color");
        var modelOrdinal = reader.GetOrdinal("model");
        var dateFromOrdinal = reader.GetOrdinal("DateFrom");
        var dateToOrdinal = reader.GetOrdinal("DateTo");
        var totaPriceOrdinal = reader.GetOrdinal("TotalPrice");

        ClientWithRentalsDTO clientWithRentalsDto = null;

        while (await reader.ReadAsync())
        {
            if (clientWithRentalsDto is not null)
            {
                clientWithRentalsDto.rentals.Add(new RentalDTO()
                {
                    vin = reader.GetString(vinOrdinal),
                    color = reader.GetString(colorOrdinal),
                    model = reader.GetString(modelOrdinal),
                    dateFrom = reader.GetDateTime(dateFromOrdinal),
                    dateTo = reader.GetDateTime(dateToOrdinal),
                    totalPrice = reader.GetInt32(totaPriceOrdinal)
                });
            }
            else
            {
                clientWithRentalsDto = new ClientWithRentalsDTO()
                {
                    id = reader.GetInt32(clientIdOrdinal),
                    firstname = reader.GetString(firstNameOrdinal),
                    lastName = reader.GetString(lastnameOrdinal),
                    address = reader.GetString(addressOrdinal),
                    rentals = new List<RentalDTO>()
                    {
                      new RentalDTO()
                      {
                          vin = reader.GetString(vinOrdinal),
                          color = reader.GetString(colorOrdinal),
                          model = reader.GetString(modelOrdinal),
                          dateFrom = reader.GetDateTime(dateFromOrdinal),
                          dateTo = reader.GetDateTime(dateToOrdinal),
                          totalPrice = reader.GetInt32(totaPriceOrdinal) 
                      }  
                    }
                };
            }
        }

        if (clientWithRentalsDto is null) throw new Exception();
        return clientWithRentalsDto;
    }
}