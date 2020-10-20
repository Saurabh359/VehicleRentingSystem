using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleService.Repository
{
    public interface IAuthRepository<T>
    {
        Task<bool> Add(T entity);
        Task<List<T>> GetAll();
        Task<List<T>> Search(int id);
        Task<T> Get(string a, string b);
    }
}
