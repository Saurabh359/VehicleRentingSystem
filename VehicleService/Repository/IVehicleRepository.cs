using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleService.Repository
{
    public interface IVehicleRepository<T> 
    {
        Task<List<T>> FindAll();
        Task<List<T>> Search(int id);
        Task<bool> Add(T entity);
    }
}
