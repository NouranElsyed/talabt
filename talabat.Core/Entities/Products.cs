using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Entities
{
    public class Products : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandsId { get; set; }

        public int CategoryId { get; set; }
        public Brands Brands { get; set; }
        public Category Category { get; set; }


    }
}
