namespace CarSharingBL.DTOs
{
    public class PassengerCreateDto : BaseDto
    {
        public int UserId { get; set; }

        public int RideId { get; set; }
    }
}
