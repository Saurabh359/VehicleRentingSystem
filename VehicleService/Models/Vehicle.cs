using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleService.Models
{
    public class Vehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int RentPerHour { get; set; }

        public string Brand { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
