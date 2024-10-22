using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Core.Entities;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.RepositoriesContext;
using talabat.Repository.Data;

namespace talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            _repositories = new Hashtable();
            _storeContext = storeContext;
        }
    
        public async Task<int> CompleteAsync() => await _storeContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()=>await _storeContext.DisposeAsync();
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type)) 
            {
                var Respositry = new GenericRepository<TEntity>(_storeContext); 
                _repositories.Add(type, Respositry);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
