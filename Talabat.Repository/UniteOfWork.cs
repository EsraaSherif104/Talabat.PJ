using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly StoreContext _dbcontext;
        private Hashtable _repositories;
        public UniteOfWork(StoreContext dbcontext)
        {
            _repositories = new Hashtable();
            this._dbcontext = dbcontext;
        }
        public async Task<int> CompleteAsync()
        {
         return  await  _dbcontext.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
           return _dbcontext.DisposeAsync();    
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(Type))
            {
                var repository = new GenericRepository<TEntity>(_dbcontext);
                _repositories.Add(Type, repository);
            }
                return _repositories[Type] as IGenericRepository<TEntity>;

        }
    }
}
