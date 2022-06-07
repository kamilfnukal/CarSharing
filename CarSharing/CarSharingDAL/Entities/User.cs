using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public int PictureId { get; set; }
        [ForeignKey(nameof(PictureId))]
        public virtual Picture UserPicture { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
    }
}
