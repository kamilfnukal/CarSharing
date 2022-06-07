namespace CarSharingBL.DTOs
{
    public class PictureDto : BaseDto
    {
        public string Url { get; set; }

        public int? CarId { get; set; }

        public int? UserId { get; set; }
    }
}
