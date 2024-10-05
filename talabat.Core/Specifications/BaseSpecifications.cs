using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Core.Specifications
{
    public class BaseSpecifications<T>: ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public BaseSpecifications() { }
        public BaseSpecifications(Expression<Func<T, bool>> critriaExpression) 
        {
            Critria = critriaExpression;
        }
    }
}
