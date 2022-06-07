namespace CarSharingBL.DTOs
{
    public class DriverDto : BaseDto
    {
        public string UserName { get; set; }

        public int Age { get; set; }

        public PictureDto UserPicture { get; set; }
    }
}
