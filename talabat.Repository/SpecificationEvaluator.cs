using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;
using talabat.Core.Specifications;

namespace talabat.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> innerQuery, ISpecification<TEntity> spec) 
        {
           var query = innerQuery;
            if (spec.Critria is not null) 
            {
                query = query.Where(spec.Critria);
            }
            if (spec.OrderBy is not null) 
            {
                query  = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            query = spec.Includes.Aggregate(query,(currentQuery,IncludeExpression)=>currentQuery.Include(IncludeExpression));
            return query;
        
        }
    }
}
