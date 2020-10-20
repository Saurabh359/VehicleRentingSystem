using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApplication.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int RentPerHour { get; set; }
        public string Brand { get; set; }
        public bool Available { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }
    }
}
