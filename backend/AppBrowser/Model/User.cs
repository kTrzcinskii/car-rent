namespace AppBrowser.Model
{
    public class User
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfLicenseObtained { get; set; }

        public required string Location { get; set; }

        public int YearsWithLicense
        {
            get
            {
                var today = DateTime.Today;
                int years = today.Year - DateOfLicenseObtained.Year;

                if (today.AddYears(-years) < DateOfLicenseObtained.Date) --years;

                return years;
            }
        }
    }
}