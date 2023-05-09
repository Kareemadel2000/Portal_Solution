using Portal.Application.Specifications;
using Portal.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdWithSpecificationsAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetAllWithSpecificationsAsync(ISpecification<T> specification);
        Task<int> GetCountAsync(ISpecification<T> specification);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
