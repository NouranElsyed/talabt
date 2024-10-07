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
        public ProductWithBrandAndCategorySpecifications(string sort):base()
        {
            Includes.Add(P=>P.Brand);
            Includes.Add(P => P.Category);
            if (!string.IsNullOrEmpty(sort)) 
            {
                switch (sort) 
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                    break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(P=>P.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

        }

    }
}
