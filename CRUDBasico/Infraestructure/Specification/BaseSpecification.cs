using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRUDBasico.Infrastructure.Specification
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {

        public Expression<Func<T, bool>> Criteria { get; internal set; }
        
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();


        public void SetCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        public virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
