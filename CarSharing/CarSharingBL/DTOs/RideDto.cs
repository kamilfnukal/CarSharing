using System;
using System.Collections.Generic;

namespace CarSharingBL.DTOs
{
    public class RideDto : BaseDto
    {
        public decimal Price { get; set; }

        public int AvailableSeats { get; set; }

        public int DriverId { get; set; }

        public DriverOfRideDto Driver { get; set; }

        public string CityFrom { get; set; }

        public string CityTo { get; set; }

        public int CarId { get; set; }

        public CarOfRideDto Car { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<PassengerDto> Passengers { get; set; }

        public ICollection<RatingDto> Ratings { get; set; }
    }
}
