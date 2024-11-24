using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        #region WithoutSpecification
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T>Spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);

        Task<int> GetCountWithSpecAsync(ISpecification<T> Spec);

        Task AddAsync(T item);
        void Update (T item);
        void Delete(T item);
    
    }
}
