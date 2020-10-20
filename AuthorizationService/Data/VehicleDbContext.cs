using AuthorizationService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Data
{
    public class VehicleDbContext:DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
