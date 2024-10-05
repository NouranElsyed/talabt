using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;
using talabat.Repository.Data;

namespace talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;
        public GenericRepository(StoreContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Products)) 
            {
                return (IEnumerable<T>)await _dbcontext.Set<Products>().Include(P=>P.Brand).Include(P => P.Category).ToListAsync();
            }
           return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            if (typeof(T) == typeof(Products))
            { return await _dbcontext.Set<Products>().Where(P=>P.Id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T; }
                return await _dbcontext.Set<T>().FindAsync(id);
        }
    }
}
