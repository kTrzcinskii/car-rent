namespace AppBrowser.Model
{
    public class User
    {
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required DateTime DateOfLicenseObtained { get; set; }
        public required string Location { get; set; }
        public virtual required List<Offer> Offers { get; set; }
        public virtual required List<Rent> Rents { get; set; }
        
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

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int years = today.Year - DateOfBirth.Year;

                if (today.AddYears(-years) < DateOfLicenseObtained.Date) --years;

                return years;
            }
        }
    }
}