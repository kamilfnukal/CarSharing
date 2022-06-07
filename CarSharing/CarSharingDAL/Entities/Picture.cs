using System.ComponentModel.DataAnnotations;

namespace CarSharingDAL.Entities
{
    public class Picture : BaseEntity
    {
        [Required]
        public string Url { get; set; }

        public int? UserId { get; set; }

        public int? CarId { get; set; }
    }
}
