using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.BaseSpec
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>>? Criteria { get; }  // Criteria 
        List<Expression<Func<T, object>>> includes { get; } //includes 

      
        Expression<Func<T, object>>? _OrderBy { get; }
        Expression<Func<T, object>>? _OrderByDesc { get; }
    }
}
