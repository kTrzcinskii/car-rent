using AppRental.Model;

namespace AppRental.DTO;

public class RentStatusDTO
{
    public required string Status { get; set; }
    public DateTime? EndDate { get; set; }

    public static RentStatusDTO FromRent(Rent rent)
    {
        return new RentStatusDTO
        {
            Status = rent.Status.ToString(),
            EndDate = rent.EndDate
        };
    }
}