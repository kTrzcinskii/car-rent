namespace AppBrowser.DTOs;

public class CreateUserDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfLicenseObtained { get; set; }
    public required string Location { get; set; }
}