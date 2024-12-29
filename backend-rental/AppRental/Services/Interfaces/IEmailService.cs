using AppRental.Model;

namespace AppRental.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendRentConfirmationEmailAsync(string recipientEmail, string confirmationLink);
        Task SendBillingEmailAsync(Rent rent);
    }
}