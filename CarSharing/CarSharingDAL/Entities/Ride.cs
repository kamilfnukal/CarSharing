using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSharingDAL.Entities
{
    public class Ride : BaseEntity
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int AvailableSeats { get; set; }

        [Required]
        public int DriverId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public virtual User Driver { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string CityFrom { get; set; }

        [Required]
        public string CityTo { get; set; }

        [Required]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
