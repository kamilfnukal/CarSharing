using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarSharingDAL.Entities
{
    public class Car : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string Brand { get; set; }
        
        [Required]
        public int Seats { get; set; }

        public int? YearOfProduction { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public int UserId { get; set; }
    }
}
