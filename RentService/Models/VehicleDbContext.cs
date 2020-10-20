using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentService.Models
{
    public class VehicleDbContext:DbContext
    {

        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
