using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentService.Repository
{
    public interface ICartRepository<T>
    {
        Task<List<int>> GetAll(int id);
        Task<bool> Add(T entity);
    }
} 
 