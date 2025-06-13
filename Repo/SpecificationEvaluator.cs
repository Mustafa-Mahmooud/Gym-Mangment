using Core.BaseSpec;
using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    internal class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecification<T> specification)
        {
            var query = InputQuery; //context.table  => Got From GenericRepo

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);  //context.table.where(Criteria)
            }

            if (specification._OrderBy is not null)
            {
                query = query.OrderBy(specification._OrderBy);
            }

            else if (specification._OrderByDesc is not null)
            {
                query = query.OrderByDescending(specification._OrderByDesc);
            }


            query = specification.includes.Aggregate(query, (current, include) => current.Include(include));





            return query;
        }
    }
}