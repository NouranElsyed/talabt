using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithBrandAndCategorySpecifications:BaseSpecifications<Products>
    {
        public ProductWithBrandAndCategorySpecifications():base()
        {
            Includes.Add(P=>P.Brand);
            Includes.Add(P => P.Category);

        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(P=>P.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

        }
    }
}
