using AppRental.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public TestController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            if(await _context.Database.CanConnectAsync())
            {
                return Ok("Connected to the database-rental.");
            }
            else
            {
                return StatusCode(500, "Connection to the database-rental failed.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> TestEmailSending()
        {
            var confirmationLink = "https://example.com";
            var message = new SendGridMessage
            {
                From = new EmailAddress(_configuration["SendGrid:FromEmail"], "Car Rental"),
                Subject = "Rent Confirmation",
                PlainTextContent = $"Confirm renting the car by clicking the link below: {confirmationLink}",
                HtmlContent = $@"
                    <p>Confirm renting the car by clicking the link below:</p>
                    <p><a href='{confirmationLink}'>Confirm Rent</a></p>"
            };
            message.AddTo(new EmailAddress("sstandsforstarlet@gmail.com"));
            message.SetClickTracking(false, false);

            var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
            var response = await client.SendEmailAsync(message);
            return Ok(response.StatusCode);
        }
    }
}