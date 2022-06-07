using System;
using System.Collections.Generic;

namespace CarSharingBL.DTOs
{
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public PictureGetDto Picture { get; set; }

        public ICollection<CarDto> Cars { get; set; }

        public ICollection<RideDto> Rides { get; set; }
    }
}
