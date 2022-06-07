using System.Collections.Generic;
namespace CarSharingBL.DTOs
{
    public class CarDto : BaseDto
    {
        public string Brand { get; set; }

        public int Seats { get; set; }

        public int YearOfProduction { get; set; }

        public int UserId { get; set; }

        public ICollection<PictureGetDto> Pictures { get; set; }
    }
}
