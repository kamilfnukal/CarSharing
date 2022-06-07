namespace CarSharingBL.DTOs
{
    public class RideCreateDto
    {
        public decimal Price { get; set; }

        public int AvailableSeats { get; set; }

        public int DriverId { get; set; }

        public int CarId { get; set; }

        public string CityFrom { get; set; }

        public string CityTo { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
    }
}
