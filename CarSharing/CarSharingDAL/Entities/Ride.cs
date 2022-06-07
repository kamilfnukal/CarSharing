using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarSharingDAL.Entities
{
    public class Ride : BaseEntity
    {
        public decimal Price { get; set; }
        
        public int DriverId { get; set; }
        [ForeignKey(nameof(DriverId))]
        public virtual User Driver { get; set; }

        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<PathInfo> Cities { get; set; }
    }
}
