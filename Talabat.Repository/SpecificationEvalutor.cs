using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        //fun to build quary dyn
       //  return await _dbcontext.Set<T>().Where(p=>p.Id==id).Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();

        public static IQueryable<T> GetQuery(IQueryable<T>inputQuery,ISpecification<T> spec)
        {
            var Query = inputQuery;// _dbcontext.Set<T>()
           if(spec.Criteria is not null)
            {

                   Query= Query.Where(spec.Criteria);//_dbcontext.Set<T>().where(p=>p.id==id)
            }
            //p=>p.productBrand ,p=>p.producttype

            Query = spec.Include.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            //_dbcontext.Set<T>().Where(p=>p.Id==id).Include(p => p.ProductBrand)
            //_dbcontext.Set<T>().Where(p=>p.Id==id).Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();



            return Query;
        }
    }

}
