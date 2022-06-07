using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSharingDAL.Entities
{
    public class Rating : BaseEntity
    {
        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }

        [Required]
        public int ForUserId { get; set; }

        [ForeignKey(nameof(ForUserId))]
        public virtual User ForUser { get; set; }

        [MaxLength(150)]
        public string Comment { get; set; }

        public int RideId { get; set; }
    }
}
