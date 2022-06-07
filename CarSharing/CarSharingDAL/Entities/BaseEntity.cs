using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarSharingDAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
