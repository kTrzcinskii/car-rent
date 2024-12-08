using AppRental.Model;

namespace AppRental.DTO;

public class RentStatusDTO
{
    public required string status;

    public static RentStatusDTO FromRent(Rent rent)
    {
        return new RentStatusDTO
        {
            status = rent.Status.ToString()
        };
    }
}