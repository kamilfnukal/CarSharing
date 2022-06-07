using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarSharingDAL.Entities
{
    public class Car : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string NumberPlate { get; set; }

        [MaxLength(25)]
        public string Brand { get; set; }
        [Required]
        public int Seats { get; set; }
        [Required]
        public int YearOfProduction { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
