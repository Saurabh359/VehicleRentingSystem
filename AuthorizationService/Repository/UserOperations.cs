using AuthorizationService.Data;
using AuthorizationService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleService.Repository;

namespace AuthorizationService.Repository
{
    public class UserOperations : IAuthRepository<User>
    {
        readonly log4net.ILog _log4net;

        public readonly VehicleDbContext vehicleDbContext;

        public UserOperations(VehicleDbContext _vehicleDbContext)
        {
            vehicleDbContext = _vehicleDbContext;
            _log4net = log4net.LogManager.GetLogger(typeof(UserOperations));
        }

        public async Task<bool> Add(User user)
        {
            try
            {
                _log4net.Info(nameof(UserOperations) + "invoked");

                vehicleDbContext.Users.Add(user);
                 int a = await vehicleDbContext.SaveChangesAsync();
                return a==1;
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(UserOperations) + "Error Message " + e.Message);
                return false;
            }
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> Search(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(string email, string password)
        {
            try
            {
                _log4net.Info(nameof(UserOperations) + "invoked");

                return await vehicleDbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(UserOperations) + "Error Message " + e.Message);
                return null;
            }

        }
    }
}
