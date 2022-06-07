namespace CarSharingBL.DTOs
{
    public class CarCreateDto : BaseDto
    {
        public string Brand { get; set; }

        public int Seats { get; set; }

        public int YearOfProduction { get; set; }

        public int UserId { get; set; }
    }
}
