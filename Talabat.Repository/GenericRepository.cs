﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        #region WithoutSpec
        public async Task<IReadOnlyList<T>> GetAllAsync()

        {
              
                return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
            // return await _dbcontext.Set<T>().Where(p=>p.Id==id).Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
        }


        #endregion
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpec(Spec).ToListAsync();
        }


        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpec(Spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpec(ISpecification<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(), Spec);
           
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpec(Spec).CountAsync();
        }

        public async Task AddAsync(T item)
        {
          await  _dbcontext.Set<T>().AddAsync(item);
        }

        public void Update(T item)
        {
            _dbcontext.Set<T>().Update(item);
        }

        public void Delete(T item)
        {
            _dbcontext.Set<T>().Remove(item);
        }
    }
   
}
