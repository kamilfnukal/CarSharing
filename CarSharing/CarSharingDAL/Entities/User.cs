using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSharingDAL.Entities
{
    public class User: BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
