using AppRental.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AppRental.Services.Implementations
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendRentConfirmationEmailAsync(string recipientEmail, string confirmationLink)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(_configuration["SendGrid:FromEmail"], "Car Rental"),
                Subject = "Rent Confirmation",
                PlainTextContent = $"Click the link below to confirm renting the car: {confirmationLink}",
                HtmlContent = $@"
                    <p>Click the link below to confirm renting the car:</p>
                    <p><a href='{confirmationLink}'>Confirm Rent</a></p>"
            };
            message.AddTo(new EmailAddress(recipientEmail));
            message.SetClickTracking(false, false);

            var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
            await client.SendEmailAsync(message);
        }

    }
}