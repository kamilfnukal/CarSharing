using System.ComponentModel.DataAnnotations.Schema;

namespace CarSharingDAL.Entities
{
    public class Passenger : BaseEntity
    {
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int? RideId { get; set; }
    }
}
