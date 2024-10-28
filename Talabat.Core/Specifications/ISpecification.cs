using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        
        // _dbcontext.Products.WHERE(P=>P.ID==ID).Include(p=>p.ProductBrand).Include(p=>p.ProductType).ToListAsync();

        //SIGN FOR PRoperty for where condition
        //expression<lambda type> criterie{}
        //lambda =>delegate[fun,predicate,action]
        //fun=> return bool or predicate retun bool

        public Expression<Func<T,bool>> Criteria { get; set; }

        //sign for property for list of include

        //List<expression<lambda>include

        public List<Expression<Func<T,object>>> Include { get; set; }

        //pro for order by(orderby(p=>p.name)
        public Expression<Func<T,object>>OrderBy { get; set; }

        //pro for order by(orderbydes(p=>p.name)
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        //take(2)
        public int Take {  get; set; }
        //skip
        public int Skip { get; set; }

        public bool IsPaginationEnable { get; set; }



    }
}
