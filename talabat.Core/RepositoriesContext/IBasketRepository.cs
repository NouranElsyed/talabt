using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Core.RepositoriesContext
{
    public interface IBasketRepository
    {
        // 3  functions [ get .. create OR update .. delete ]
        public Task<CustomerBasket?>  GetBasketAsync(string id);
        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        public Task<bool> DeleteBasketAsync(string id);

    }
}
