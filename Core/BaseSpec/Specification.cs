using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.BaseSpec
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }

        public List<Expression<Func<T, object>>> includes { get; set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> _OrderBy { get; set; }
        public Expression<Func<T, object>> _OrderByDesc { get; set; }



        public void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            _OrderBy = OrderBy;
        }


        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDesc)
        {
            _OrderByDesc = OrderByDesc;
        }
        public Specification()
        {
            Criteria = null;

        }

        public Specification(Expression<Func<T, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
    }

}

