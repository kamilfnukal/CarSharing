using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarSharingDAL.Entities
{
    public class Passenger : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int FromId { get; set; }
        [ForeignKey(nameof(FromId))]
        public virtual PathInfo From { get; set; }

        public int ToId { get; set; }
        [ForeignKey(nameof(ToId))]
        public virtual PathInfo To { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
