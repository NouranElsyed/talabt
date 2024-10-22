using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.Specifications;

namespace talabat.Core.RepositoriesContext
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetWithSpecAsync(ISpecification<T> spec);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task AddAsync(T item);
        void Update(T item); 
        void Delete(T item);

    }
}
