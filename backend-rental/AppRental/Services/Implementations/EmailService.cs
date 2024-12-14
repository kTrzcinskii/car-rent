using AppRental.Model;
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

        public async Task SendBillingEmailAsync(Rent rent)
        {
            var numDays = (rent.EndDate - rent.StartDate)?.Days + 1;
            var rentalCost = rent.Offer.CostPerDay * numDays;
            var insuranceCost = rent.Offer.InsuranceCostPerDay * numDays;
            var total = rentalCost + insuranceCost;

            var brand = rent.Offer.Car.Brand;
            var model = rent.Offer.Car.Model;

            var message = new SendGridMessage
            {
                From = new EmailAddress(_configuration["SendGrid:FromEmail"], "Car Rental"),
                Subject = "Billing Details",
                PlainTextContent = $@"
                    Dear {rent.FirstName} {rent.LastName},

                    Thank you for choosing our car rental service. Your return of the car {brand} {model} was successful. Below are your billing details:

                    Rental Cost: ${rentalCost}
                    Insurance Cost: ${insuranceCost}
                    Total: ${total}

                    Best regards,
                    Car Rental Team
                ",
                HtmlContent = $@"
                    <p>Dear {rent.FirstName} {rent.LastName},</p>
                    <p>Thank you for choosing our car rental service. Your return of the car {brand} {model} was successful. Below are your billing details:</p>
                    <table>
                        <tr>
                            <td>Rental Cost:</td>
                            <td>${rentalCost}</td>
                        </tr>
                        <tr>
                            <td>Insurance Cost:</td>
                            <td>${insuranceCost}</td>
                        </tr>
                        <tr>
                            <td><strong>Total:</strong></td>
                            <td><strong>${total}</strong></td>
                        </tr>
                    </table>
                    <p>Best regards,<br>Car Rental Team</p>
                "
            };
            message.AddTo(new EmailAddress(rent.Email));
            message.SetClickTracking(false, false);

            var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
            await client.SendEmailAsync(message);
        }

    }
}