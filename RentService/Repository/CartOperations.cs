using Microsoft.EntityFrameworkCore;
using RentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentService.Repository
{
    public class CartOperations : ICartRepository<Cart>
    {
        readonly log4net.ILog _log4net;

        private readonly VehicleDbContext context;

        public CartOperations(VehicleDbContext vehicleDbContext)
        {
            context = vehicleDbContext;
            _log4net = log4net.LogManager.GetLogger(typeof(CartOperations));
        }

        public async Task<bool> Add(Cart cart)
        {
            try
            {
                _log4net.Info(nameof(CartOperations) + "invoked");

                context.Carts.Add(cart);
                int a = await context.SaveChangesAsync();
                return a == 1;

            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(CartOperations) + "Error Message " + e.Message);
                return false;
            }
        }

        public async Task<List<Cart>> GetAll(int id)
        {
            try
            {
                _log4net.Info(nameof(CartOperations) + "invoked");

                var cart = context.Carts.Where(x => x.UserId.Equals(id)).Include(x => x.Vehicle);
                return await cart.ToListAsync();
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(CartOperations) + "Error Message " + e.Message);
                return null;
            }
        }
    }
}  