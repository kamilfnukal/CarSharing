namespace CarSharingBL.DTOs
{
    public class UserRegistrationDto : BaseDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string BirthDate { get; set; }
    }
}
