using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentService.Models
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public User User { get; set; }
        [Required]
        public int UserId { get; set; }

        public Vehicle Vehicle { get; set; }
        [Required]
        public int VehicleId { get; set; }

        [Required] 
        public DateTime Booking { get; set; }

        [Required]
        public int Hours { get; set; }
    }
}
