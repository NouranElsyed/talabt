﻿using System;
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
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        //public BaseSpecifications() { }
        public BaseSpecifications(Expression<Func<T, bool>> critriaExpression) 
        {
            Critria = critriaExpression;
        }
        public void AddOrderBy(Expression<Func<T,object>> orderbyexpression)
        {
            OrderBy = orderbyexpression;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderbydescendingexpression) 
        {
            OrderByDescending = orderbydescendingexpression;
        }
    }
}
