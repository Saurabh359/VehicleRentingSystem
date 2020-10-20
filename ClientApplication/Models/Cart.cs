using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApplication.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime Booking { get; set; }
        public int Hours { get; set; }
    }
}
