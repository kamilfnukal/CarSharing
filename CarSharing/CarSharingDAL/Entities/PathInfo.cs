using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarSharingDAL.Entities
{
    public class PathInfo : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string CityName { get; set; }
        public DateTime Time { get; set; }
    }
}
