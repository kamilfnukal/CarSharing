namespace CarSharingBL.DTOs
{
    public class RatingDto : BaseDto
    {
        public int Rate { get; set; }

        public int ForUserId { get; set; }

        public string Comment { get; set; }

        public int RideId { get; set; }
    }
}
