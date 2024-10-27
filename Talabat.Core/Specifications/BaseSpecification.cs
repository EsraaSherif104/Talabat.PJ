using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set ; }
        public List<Expression<Func<T, object>>> Include { get ; set ; } = new List<Expression<Func<T, object>>>();

        //get all
        public BaseSpecification()
        {
          //  Include = new List<Expression<Func<T, object>>>();
        }
        //get by id
        public BaseSpecification(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria= CriteriaExpression;
           // Include= new List<Expression<Func<T, object>>>();

        }
    }
}
