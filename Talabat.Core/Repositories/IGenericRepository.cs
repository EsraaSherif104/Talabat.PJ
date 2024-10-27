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
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T>Spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);


    }
}
