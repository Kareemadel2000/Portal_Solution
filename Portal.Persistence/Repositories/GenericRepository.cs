using Microsoft.EntityFrameworkCore;
using Portal.Application.Contracts;
using Portal.Persistence.DataBase;
using Portal.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Application.Specifications;

namespace Portal.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecificationsAsync(ISpecification<T> specification)
            => await ApplySpecifications(specification).ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<T> GetByIdWithSpecificationsAsync(ISpecification<T> specification)
            => await ApplySpecifications(specification).FirstOrDefaultAsync();

        public async Task<int> GetCountAsync(ISpecification<T> specification)
            => await ApplySpecifications(specification).CountAsync();

        public void Update(T entity)
             => _context.Set<T>().Update(entity);

        private IQueryable<T> ApplySpecifications(ISpecification<T> specification)
            => SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specification);
    }
}
