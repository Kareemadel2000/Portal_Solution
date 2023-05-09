using Microsoft.EntityFrameworkCore;
using Portal.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Specifications
{
    public class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification) 
        {
            var query = inputQuery;

            if(specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if(specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            if(specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            if (specification.IsPaginationEnabled)
                query = query.Skip(specification.Skip).Take(specification.Take);

            query = specification.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            return query;
        }
    }
}
