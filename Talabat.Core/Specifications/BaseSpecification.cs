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
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool IsPaginationEnable { get ; set ; }

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

        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy= OrderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDescending= OrderByDescExpression;
        }

        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnable = true;
            Skip=skip;
            Take=take;
        }
    }
}
