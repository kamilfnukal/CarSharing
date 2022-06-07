using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarSharingDAL.Entities
{
    public class Rating : BaseEntity
    {
        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
        [MaxLength(150)]
        public string Comment { get; set; }
    }
}
