using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Specifications.ProductSpecifications
{
    public class ProductSpecParams
    {

        public string? sort { get; set;}
        public int? BrandId { get; set;}
        public int? CategoryId { get; set; }
        public int? PageSize{ get; set; }
        public int? PageIndex{ get; set; }

     }  
}
